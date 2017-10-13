using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using XCommon.Application.Executes;
using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.Core.Extensions;
using XCommon.CodeGenerator.TypeScript.Implementation.Helper;
using XCommon.Extensions.String;
using XCommon.Patterns.Ioc;
using XCommon.Patterns.Repository.Entity;
using XCommon.Util;

namespace XCommon.CodeGenerator.TypeScript.Implementation
{
	public class TypeScriptEntityWriter : BaseWriter, ITypeScriptEntityWriter
	{
		[Inject]
		private ITypeScriptIndexExport TypeScriptIndexExport { get; set; }

		private List<string> GeneratedEntities { get; set; }

		private List<TypeScriptClass> TSClass { get; set; }

		private List<TypeScriptEnum> TSEnums { get; set; }

		private void LoadEnums()
		{
			var types = Config.TypeScript.Entity.Assemblys.SelectMany(c => c.GetTypes()).Where(c => c.GetTypeInfo().IsEnum).ToList();

			types.AddRange(Config.TypeScript.Entity.TypesExtra.Where(c => c.GetTypeInfo().IsEnum));
			types.Add(typeof(ExecuteMessageType));
			types.Add(typeof(EntityAction));
			types = types.Distinct().ToList();

			foreach (var type in types)
			{
				var enumProperty = new TypeScriptEnum
				{
					Name = type.Name,
					Type = type,
					Values = new Dictionary<string, int>()
				};

				var enumValues = System.Enum.GetValues(type);

				for (var i = 0; i < enumValues.Length; i++)
				{
					enumProperty.Values.Add(enumValues.GetValue(i).ToString(), (int)enumValues.GetValue(i));
				}

				TSEnums.Add(enumProperty);
			}
		}

		private void LoadProperties()
		{
			var nullablePropertys = new string[] { "Keys", "Keys", "PageNumber", "PageSize" };

			var types = Config.TypeScript.Entity.Assemblys
				.SelectMany(c => c.GetTypes())
				.Where(c => !c.GetTypeInfo().IsAbstract && !c.GetTypeInfo().IsInterface && !c.GetTypeInfo().IsEnum)
				.ToList();

			types.AddRange(Config.TypeScript.Entity.TypesExtra.Where(c => !c.GetTypeInfo().IsAbstract && !c.GetTypeInfo().IsInterface && !c.GetTypeInfo().IsEnum));

			foreach (var type in types.Distinct())
			{
				if (type.Namespace.IsEmpty())
				{
					continue;
				}

				var tsClass = new TypeScriptClass
				{
					Class = type.Name,
					FileName = GetFileName(type.Namespace),
				};

				foreach (var property in type.GetProperties())
				{
					if (property.GetCustomAttributes<IgnoreDataMemberAttribute>().Count() > 0)
					{
						continue;
					}

					var isGeneric = property.PropertyType.IsGenericParameter && !property.Name.Contains("List");
					var enumProperty = TSEnums.FirstOrDefault(c => c.Type == (Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType));
					var typeName = enumProperty != null
							? enumProperty.Name
							: GetPropertyType(property.PropertyType, tsClass, isGeneric);

					if (enumProperty != null)
					{
						tsClass.Imports.Add(new TypeScriptImport
						{
							Class = enumProperty.Name,
							File = "enum.ts"
						});
					}

					tsClass.Properties.Add(new TypeScriptProperty
					{
						Name = property.Name,
						Nullable = nullablePropertys.Contains(property.Name) 
									|| type.Name.EndsWith("Filter") 
									|| type.GetCustomAttributes<TSNullableAttribute>().Count() > 0
									|| Nullable.GetUnderlyingType(property.PropertyType) != null,
						Generic = isGeneric,
						Type = typeName
					});
				}

				TSClass.Add(tsClass);
			}
		}

		private void ProcessEnum()
		{
			var builder = new StringBuilderIndented();

			builder
				.GenerateFileMessage();

			foreach (var enumItem in TSEnums)
			{
				builder
					.AppendLine($"export enum {enumItem.Name} {{")
					.IncrementIndent();

				foreach (var values in enumItem.Values)
				{
					builder
						.AppendLine($"{values.Key} = {values.Value},");
				}

				builder
					.DecrementIndent()
					.AppendLine("}")
					.AppendLine();
			}

			Writer.WriteFile(Config.TypeScript.Entity.Path.ToLower(), "enum.ts", builder, true);
		}

		private void ProcessTypes()
		{
			foreach (var file in TSClass.Select(c => c.FileName).Distinct().OrderBy(c => c))
			{
				var fileName = file.GetSelector();
				var importItems = TSClass.Where(c => c.FileName == file).SelectMany(c => c.Imports).ToList();

				var builder = new StringBuilderIndented();

				builder
					.GenerateFileMessage();

				if (importItems.Any())
				{
					var imported = false;

					foreach (var importFile in importItems.Where(c => c.File != file).Select(c => c.File).Distinct())
					{
						imported = true;

						var classes = importItems.Where(c => c.File == importFile).Select(c => c.Class).Distinct().ToArray();
						var import = string.Join(", ", classes);

						builder
							.AppendLine($"import {{ {import} }} from \"./{importFile.Replace(".ts", string.Empty)}\";");
					}

					if (imported)
					{
						builder
							.AppendLine();
					}
				}

				foreach (var className in TSClass.Where(c => c.FileName == file).Select(c => c.Class).Distinct().OrderBy(c => c))
				{
					var tsClass = TSClass
						.Where(c => c.FileName == file && c.Class == className)
						.FirstOrDefault();

					var generics = tsClass.Properties
						.Where(c => c.Generic)
						.Select(c => c.Type)
						.ToList();

					var classNamePrint = tsClass.Class;

					if (generics.Count > 0)
					{
						classNamePrint = classNamePrint.Substring(0, classNamePrint.IndexOf('`'));
						classNamePrint += "<";
						classNamePrint += string.Join(", ", generics);
						classNamePrint += ">";
					}

					builder
						.AppendLine($"export interface I{classNamePrint} {{")
						.IncrementIndent();

					foreach (var property in tsClass.Properties)
					{
						var nullable = property.Nullable ? "?" : "";

						builder
							.AppendLine($"{property.Name}{nullable}: {property.Type}; ");
					}

					builder
						.DecrementIndent()
						.AppendLine("}")
						.AppendLine();
				}

				if (GeneratedEntities.Contains(fileName))
				{
					throw new Exception($"Two file with same name cannot be generated: {fileName}");
				}

				GeneratedEntities.Add(fileName);
				Writer.WriteFile(Config.TypeScript.Entity.Path.ToLower(), fileName, builder, true);
			}
		}

		private void ProcessExtras()
		{
			if (!Config.TypeScript.Entity.IncludeUtils)
				return;

			var builder = new StringBuilderIndented();

			builder
				.AppendLine("export interface Map<TValue> {")
				.IncrementIndent()
				.AppendLine("[K: string]: TValue;")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine()
				.AppendLine("export interface KeyValue<TKey, TValue> {")
				.IncrementIndent()
				.AppendLine("Key: TKey;")
				.AppendLine("Value: TValue;")
				.DecrementIndent()
				.AppendLine("}");

			Writer.WriteFile(Config.TypeScript.Entity.Path.ToLower(), "entity-util.ts", builder, false);
		}

		private string GetFileName(string nameSpace)
		{
			var overRide = Config.TypeScript.Entity.NameOverride.FirstOrDefault(c => c.NameSpace == nameSpace);

			if (overRide != null)
			{
				return $"{overRide.FileName.ToLower()}.ts";
			}

			var result = nameSpace.Split('.').Last(c => c != "Filter");
			return $"{result}.ts";
		}

		private string GetPropertyType(Type type, TypeScriptClass tsClass, bool generic)
		{
			var attr = type.GetCustomAttribute<TSCastAttribute>();

			if (attr != null)
			{
				return attr.Type;
			}

			var underlyingType = Nullable.GetUnderlyingType(type);
			var currentType = underlyingType ?? type;

			if (currentType.GetTypeInfo().IsGenericType && currentType.Name.Contains("List"))
			{
				try
				{
					var genericType = currentType.GenericTypeArguments.FirstOrDefault();
					return string.Format("Array<{0}>", GetPropertyType(genericType, tsClass, generic));
				}
				catch (Exception ex)
				{
					return ex.Message;
				}
			}

			if (generic)
			{
				return type.Name;
			}

			if (currentType.GetTypeInfo().IsEnum)
			{
				return currentType.Name;
			}

			if (currentType.Name.Contains("Entity") || currentType.Name == "ExecuteMessage")
			{
				tsClass.Imports.Add(new TypeScriptImport
				{
					Class = $"I{currentType.Name}",
					File = GetFileName(currentType.Namespace)
				});

				return $"I{currentType.Name}";
			}

			switch (currentType.Name)
			{
				case "Int32":
				case "Int64":
				case "Decimal":
				case "Single":
					return "number";
				case "String":
				case "Guid":
					return "string";
				case "DateTime":
					return "Date";
				case "Boolean":
					return "boolean";
				default:
					return "Não identificado: " + currentType.Name;
			}
		}

		public void Run()
		{
			TSClass = new List<TypeScriptClass>();
			TSEnums = new List<TypeScriptEnum>();

			GeneratedEntities = new List<string>();

			LoadEnums();
			LoadProperties();

			if (!Directory.Exists(Config.TypeScript.Entity.Path))
			{
				Directory.CreateDirectory(Config.TypeScript.Entity.Path);
			}

			ProcessExtras();
			ProcessEnum();
			ProcessTypes();

			TypeScriptIndexExport.Run(Config.TypeScript.Entity.Path);

			Console.WriteLine("Generate entity typescript - OK");
		}
	}
}
