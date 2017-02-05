using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using XCommon.Application.Executes;
using XCommon.CodeGenerator.Angular.Extensions;
using XCommon.CodeGenerator.Angular.Writter;
using XCommon.CodeGenerator.Core.Util;
using XCommon.CodeGenerator.CSharp.Extensions;
using XCommon.CodeGenerator.TypeScript.Configuration;
using XCommon.Extensions.Util;
using XCommon.Util;

namespace XCommon.CodeGenerator.TypeScript.Writter
{
    public class Entities : FileWriter
    {
        private TypeScriptEntity Config { get; set; }

        private IndexExport Index { get; set; } = new IndexExport();

        private List<string> GeneratedEntities { get; set; }

		private List<TypeScriptClass> TSClass { get; set; }

		private List<TypeScriptEnum> TSEnums { get; set; }

		private string GetFileName(string nameSpace)
		{
            var overRide = Config.NameOveride.FirstOrDefault(c => c.NameSpace == nameSpace);

            if (overRide != null)
                return $"{overRide.FileName.ToLower()}.ts";

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

		private void LoadEnums()
		{
			var types = Config.Assemblys.SelectMany(c => c.GetTypes()).Where(c => c.GetTypeInfo().IsEnum).ToList();

			types.AddRange(Config.TypesExtra.Where(c => c.GetTypeInfo().IsEnum));
			types.Add(typeof(ExecuteMessageType));
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

		private void LoadProperties()
		{
			var nullablePropertys = new string[] { "Keys", "Keys", "PageNumber", "PageSize" };

			List<Type> types = Config.Assemblys
				.SelectMany(c => c.GetTypes())
				.Where(c => !c.GetTypeInfo().IsAbstract && !c.GetTypeInfo().IsInterface && !c.GetTypeInfo().IsEnum)
				.ToList();

			types.AddRange(Config.TypesExtra.Where(c => !c.GetTypeInfo().IsAbstract && !c.GetTypeInfo().IsInterface && !c.GetTypeInfo().IsEnum));

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

		private void ProcessEnum()
		{
			StringBuilderIndented builder = new StringBuilderIndented();

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
            
            WriteFile(Config.Path.ToLower(), "enum.ts", builder);
		}

		private void ProcessTypes()
		{
			foreach (var file in TSClass.Select(c => c.FileName).Distinct().OrderBy(c => c))
			{
				var fileName = file.GetSelector();
				var importItems = TSClass.Where(c => c.FileName == file).SelectMany(c => c.Imports).ToList();

				StringBuilderIndented builder = new StringBuilderIndented();

                builder
                    .GenerateFileMessage();

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

                if (GeneratedEntities.Contains(fileName))
                {
                    throw new Exception($"Two file with same name cannot be generated: {fileName}");
                }

                GeneratedEntities.Add(fileName);
                WriteFile(Config.Path.ToLower(), fileName, builder);
			}
		}

		internal void Run(TypeScriptEntity config, IndexExport index)
		{
            Config = config;

			TSClass = new List<TypeScriptClass>();
			TSEnums = new List<TypeScriptEnum>();

            GeneratedEntities = new List<string>();

			LoadEnums();
			LoadProperties();

			if (!Directory.Exists(config.Path))
				Directory.CreateDirectory(config.Path);

			ProcessEnum();
			ProcessTypes();

			Index.Run(config.Path);

			index.Run(config.Path);
			Console.WriteLine("Generate entity typescript - OK");
		}
	}
}
