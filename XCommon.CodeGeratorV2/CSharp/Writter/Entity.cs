using System.Collections.Generic;
using System.IO;
using System.Linq;
using XCommon.CodeGeratorV2.Core.Entity;
using XCommon.CodeGeratorV2.Core.Util;
using XCommon.CodeGeratorV2.CSharp.Configuration;
using XCommon.CodeGeratorV2.CSharp.Extensions;
using XCommon.Util;

namespace XCommon.CodeGeratorV2.CSharp.Writter
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

			var nameSpace = new List<string> { "System", "XCommon.Patterns.Repository.Entity", "XCommon.Util" };
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
				.AppendLine("[Ignore]")
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
