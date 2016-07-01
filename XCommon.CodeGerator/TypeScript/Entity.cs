using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using XCommon.Extensions.String;
using XCommon.Util;

namespace XCommon.CodeGerator.TypeScript
{
	internal class Entity
	{
		private Configuration.ConfigEntity Config => Generator.Configuration.Entity;

		private class EntityProperty
		{
			public bool Nullable { get; set; }

			public string FileName { get; set; }

			public string Class { get; set; }

			public string Name { get; set; }

			public string Type { get; set; }

			public string Import { get; set; }

            public bool Generic { get; set; }
        }

		private class EnumProperty
		{
			public string Name { get; set; }

			public Type Type { get; set; }

			public Dictionary<string, int> Values { get; set; }
		}

		private List<EntityProperty> AssemblyProperties { get; set; }

		private List<EnumProperty> AssemblyEnums { get; set; }

		private string GetFileName(string nameSpace)
		{
			var result = nameSpace.Split('.').Last(c => c != "Filter");
			return $"{result}.ts";
		}

		private string GetPropertyType(Type type, bool generic)
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
					return string.Format("Array<{0}>", GetPropertyType(genericType, generic));
				}
				catch (Exception ex)
				{
					return ex.Message;
				}

			}

            if (generic)
                return type.Name;

            if (currentType.Name.Contains("Entity") || currentType.Name == "ExecuteMessage")
			{
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
			types.Add(typeof(Patterns.Repository.Executes.ExecuteMessageType));
			types.Add(typeof(Patterns.Repository.Entity.EntityAction));

			foreach (var type in types)
			{
				var enumProperty = new EnumProperty
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

				AssemblyEnums.Add(enumProperty);
			}
		}

		private void LoadProperties()
		{
			var nullablePropertys = new string[] { "Keys", "Keys", "PageNumber", "PageSize" };

			List<Type> types = Config.Assemblys
				.SelectMany(c => c.GetTypes())
				.Where(c => !c.CheckIsAbstract() && !c.CheckIsInterface())
				.ToList();

			types.AddRange(Config.TypesExtra.Where(c => !c.CheckIsAbstract() && !c.CheckIsInterface() && !c.GetTypeInfo().IsEnum));

			foreach (var type in types.Distinct())
			{
				foreach (var property in type.GetProperties())
				{
					if (property.GetCustomAttributes<IgnoreAttribute>().Count() > 0)
						continue;

                    var isGeneric = property.PropertyType.IsGenericParameter && !property.Name.Contains("List");
                    var enumProperty = AssemblyEnums.FirstOrDefault(c => c.Type == property.PropertyType);
                    var typeName = enumProperty != null
                            ? enumProperty.Name
                            : GetPropertyType(property.PropertyType, isGeneric);

                    AssemblyProperties.Add(new EntityProperty
					{
						Class = type.Name,
						FileName = GetFileName(type.Namespace),
						Name = property.Name,
						Nullable = nullablePropertys.Contains(property.Name) || type.Name.EndsWith("Filter") || Nullable.GetUnderlyingType(property.PropertyType) != null,
                        Generic = isGeneric,
						Import = enumProperty != null
							? enumProperty.Name
							: string.Empty,
						Type = typeName
					});
				}
			}
		}

		private void ProcessEnum()
		{
			var fileName = Path.Combine(Config.Path, "Enum.ts");
			StringBuilderIndented builder = new StringBuilderIndented();

			foreach (var enumItem in AssemblyEnums)
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

			File.WriteAllText(fileName, builder.ToString());
		}

		private void ProcessTypes()
		{
			foreach (var file in AssemblyProperties.Select(c => c.FileName).Distinct().OrderBy(c => c))
			{
				var fileName = Path.Combine(Config.Path, file);
				var importItems = AssemblyProperties.Where(c => c.FileName == file).Select(c => c.Import).Where(c => c.IsNotEmpty()).Distinct().OrderBy(c => c).ToArray();
				var import = string.Join(", ", importItems);

				StringBuilderIndented builder = new StringBuilderIndented();

				if (import.IsNotEmpty())
				{
					builder
						.AppendLine($"import {{ {import} }} from \"./Enum\";")
						.AppendLine();
				}

				foreach (var className in AssemblyProperties.Where(c => c.FileName == file).Select(c => c.Class).Distinct().OrderBy(c => c))
				{
                    var generics = AssemblyProperties
                        .Where(c => c.FileName == file && c.Class == className && c.Generic)
                        .Select(c => c.Type)
                        .ToList();

                    string classNamePrint = className;

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

					foreach (var property in AssemblyProperties.Where(c => c.FileName == file && c.Class == className))
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

				File.WriteAllText(fileName, builder.ToString());
			}
		}

		internal void Run()
		{
			AssemblyProperties = new List<EntityProperty>();
			AssemblyEnums = new List<EnumProperty>();

			LoadEnums();
			LoadProperties();

			if (!Directory.Exists(Config.Path))
				Directory.CreateDirectory(Config.Path);

			ProcessEnum();
			ProcessTypes();

			Console.WriteLine("Typescript entity interfaces are up to date");
		}
	}
}
