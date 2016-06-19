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

		private string GetPropertyType(Type type)
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
					return string.Format("Array<{0}>", GetPropertyType(genericType));
				}
				catch (Exception ex)
				{
					return ex.Message;
				}

			}

			if (currentType.Name.Contains("Entity"))
			{
				return currentType.Name;
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

			foreach (var type in Config.Assemblys.SelectMany(c => c.GetTypes()).Where(c => !c.CheckIsAbstract() && !c.CheckIsInterface()))
			{
				foreach (var property in type.GetProperties())
				{
					if (property.GetCustomAttributes<IgnoreAttribute>().Count() > 0)
						continue;

					var enumProperty = AssemblyEnums.FirstOrDefault(c => c.Type == property.PropertyType);

					AssemblyProperties.Add(new EntityProperty
					{
						Class = type.Name,
						FileName = GetFileName(type.Namespace),
						Name = property.Name,
						Nullable = nullablePropertys.Contains(property.Name) || type.Name.EndsWith("Filter") || Nullable.GetUnderlyingType(property.PropertyType) != null,
						Import = enumProperty != null
							? enumProperty.Name
							: string.Empty,
						Type = enumProperty != null
							? enumProperty.Name
							: GetPropertyType(property.PropertyType)
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

				builder
					.AppendLine($"import {{{import}}} from \"./Enum\";")
					.AppendLine();

				foreach (var className in AssemblyProperties.Where(c => c.FileName == file).Select(c => c.Class).Distinct().OrderBy(c => c))
				{
					builder
						.AppendLine($"export interface I{className} {{")
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
