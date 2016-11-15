using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using XCommon.CodeGerator.Angular.Extensions;
using XCommon.CodeGerator.Angular.Writter;
using XCommon.CodeGerator.Core.Util;
using XCommon.CodeGerator.TypeScript.Configuration;
using XCommon.Util;
using XCommon.Extensions.Util;

namespace XCommon.CodeGerator.TypeScript.Writter
{
    public class Entities : FileWriter
    {
		private IndexExport Index { get; set; } = new IndexExport();

		private List<TypeScriptClass> TSClass { get; set; }

		private List<TypeScriptEnum> TSEnums { get; set; }

		private string GetFileName(string nameSpace)
		{
			var result = nameSpace.Split('.').Last(c => c != "Filter");
			return $"{result}.ts";
		}

		private string GetPropertyType(Type type, TypeScriptClass tsClass, bool generic)
		{
			var underlyingType = Nullable.GetUnderlyingType(type);

			var currentType = underlyingType == null
				? type
				: underlyingType;

			if (currentType.GetTypeInfo().IsGenericType && currentType.Name.Contains("List"))
			{
				try
				{
					Type genericType = currentType.GenericTypeArguments.FirstOrDefault();
					return string.Format("Array<{0}>", GetPropertyType(genericType, tsClass, generic));
				}
				catch (Exception ex)
				{
					return ex.Message;
				}
			}

			if (generic)
				return type.Name;

			if (currentType.GetTypeInfo().IsEnum)
				return currentType.Name;

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

		private void LoadEnums(TypeScriptEntity config)
		{
			var types = config.Assemblys.SelectMany(c => c.GetTypes()).Where(c => c.GetTypeInfo().IsEnum).ToList();

			types.AddRange(config.TypesExtra.Where(c => c.GetTypeInfo().IsEnum));
			types.Add(typeof(Patterns.Repository.Executes.ExecuteMessageType));
			types.Add(typeof(Patterns.Repository.Entity.EntityAction));
			types = types.Distinct().ToList();

			foreach (var type in types)
			{
				var enumProperty = new TypeScriptEnum
				{
					Name = type.Name,
					Type = type,
					Values = new Dictionary<string, int>()
				};

				System.Array enumValues = System.Enum.GetValues(type);

				for (int i = 0; i < enumValues.Length; i++)
				{
					enumProperty.Values.Add(enumValues.GetValue(i).ToString(), (int)enumValues.GetValue(i));
				}

				TSEnums.Add(enumProperty);
			}
		}

		private void LoadProperties(TypeScriptEntity config)
		{
			var nullablePropertys = new string[] { "Keys", "Keys", "PageNumber", "PageSize" };

			List<Type> types = config.Assemblys
				.SelectMany(c => c.GetTypes())
				.Where(c => !c.GetTypeInfo().IsAbstract && !c.GetTypeInfo().IsInterface && !c.GetTypeInfo().IsEnum)
				.ToList();

			types.AddRange(config.TypesExtra.Where(c => !c.GetTypeInfo().IsAbstract && !c.GetTypeInfo().IsInterface && !c.GetTypeInfo().IsEnum));

			foreach (var type in types.Distinct())
			{
				TypeScriptClass tsClass = new TypeScriptClass
				{
					Class = type.Name,
					FileName = GetFileName(type.Namespace),
				};

				foreach (var property in type.GetProperties())
				{
					if (property.GetCustomAttributes<IgnoreDataMemberAttribute>().Count() > 0)
						continue;

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
						Nullable = nullablePropertys.Contains(property.Name) || type.Name.EndsWith("Filter") || Nullable.GetUnderlyingType(property.PropertyType) != null,
						Generic = isGeneric,
						Type = typeName
					});
				}

				TSClass.Add(tsClass);
			}
		}

		private void ProcessEnum(TypeScriptEntity config)
		{
			StringBuilderIndented builder = new StringBuilderIndented();

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

			WriteFile(config.Path.ToLower(), "enum.ts", builder);
		}

		private void ProcessTypes(TypeScriptEntity config)
		{

			foreach (var file in TSClass.Select(c => c.FileName).Distinct().OrderBy(c => c))
			{
				var fileName = file.GetSelector();
				var importItems = TSClass.Where(c => c.FileName == file).SelectMany(c => c.Imports).ToList();

				StringBuilderIndented builder = new StringBuilderIndented();

				if (importItems.Any())
				{
					bool imported = false;

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

					string classNamePrint = tsClass.Class;

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
						string nullable = property.Nullable ? "?" : "";

						builder
							.AppendLine($"{property.Name}{nullable}: {property.Type}; ");
					}

					builder
						.DecrementIndent()
						.AppendLine("}")
						.AppendLine();
				}

				WriteFile(config.Path.ToLower(), fileName, builder);
			}
		}

		internal void Run(TypeScriptEntity config, IndexExport index)
		{
			TSClass = new List<TypeScriptClass>();
			TSEnums = new List<TypeScriptEnum>();

			LoadEnums(config);
			LoadProperties(config);

			if (!Directory.Exists(config.Path))
				Directory.CreateDirectory(config.Path);

			ProcessEnum(config);
			ProcessTypes(config);

			Index.Run(config.Path);

			index.Run(config.Path);
			Console.WriteLine("Generate entity typescript - OK");
		}
	}
}
