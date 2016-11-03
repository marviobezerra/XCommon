using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XCommon.CodeGerator.Core.Entity;
using XCommon.CodeGerator.Core.Util;
using XCommon.CodeGerator.CSharp.Configuration;
using XCommon.CodeGerator.CSharp.Extensions;
using XCommon.Util;

namespace XCommon.CodeGerator.CSharp.Writter
{
	internal class Entity : FileWriter
	{
		public void Run(CSharpConfig config)
		{
			foreach (var group in config.DataBaseItems)
			{
				foreach (var item in group.Items)
				{
					GenerateEntity(config, group, item);
					GenerateFilter(config, group, item);
				}
			}

			Console.WriteLine("Generate entity code - OK");
			Console.WriteLine("Generate entity code [filter] - OK");
		}

		private void GenerateFilter(CSharpConfig config, ItemGroup group, Item item)
		{
			string path = Path.Combine(config.EntrityPath, group.Name, "Filter");
			string file = $"{item.Name}Filter.cs";

			if (File.Exists(Path.Combine(path, file)))
				return;

			var nameSpace = new List<string> { "System", "XCommon.Patterns.Repository.Entity" };

			StringBuilderIndented builder = new StringBuilderIndented();

			builder
				.ClassInit($"{item.Name}Filter", "FilterBase", $"{config.EntrityNameSpace}.{group.Name}.Filter", ClassVisility.Public, nameSpace.ToArray())
				.ClassEnd();

			WriteFile(path, file, builder);
		}

		private void GenerateEntity(CSharpConfig config, ItemGroup group, Item item)
		{
			string path = Path.Combine(config.EntrityPath, group.Name, "Auto");
			string file = $"{item.Name}Entity.cs";

			var nameSpace = new List<string> { "System", "XCommon.Patterns.Repository.Entity", "System.Runtime.Serialization" };
			nameSpace.AddRange(item.Properties.Where(c => c.NameGroup != group.Name).Select(c => c.NameGroup));

			StringBuilderIndented builder = new StringBuilderIndented();

			builder
				.GenerateFileMessage()
				.ClassInit($"{item.Name}Entity", "EntityBase", $"{config.EntrityNameSpace}.{group.Name}", ClassVisility.Public, true, nameSpace.ToArray());

			foreach (var column in item.Properties)
			{
				builder
					.AppendLine($"public {column.Type} {column.Name} {{ get; set; }}")
					.AppendLine();
			}

			builder
				.AppendLine()
				.AppendLine("[IgnoreDataMember]")
				.AppendLine("public override Guid Key")
				.AppendLine("{")
				.IncrementIndent()
				.AppendLine("get")
				.AppendLine("{")
				.IncrementIndent()
				.AppendLine($"return {item.NameKey};")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine("set")
				.AppendLine("{")
				.IncrementIndent()
				.AppendLine($"{item.NameKey} = value;")
				.DecrementIndent()
				.AppendLine("}")
				.DecrementIndent()
				.AppendLine("}")
				.ClassEnd();

			WriteFile(path, file, builder);
		}
	}
}
