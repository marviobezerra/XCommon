using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using XCommon.CodeGerator.Entity;
using XCommon.CodeGerator.Extensions;
using XCommon.Util;

namespace XCommon.CodeGerator.Business
{
	internal class BusinessEntity
    {
		private Configuration.ConfigBusiness Config => Generator.Configuration.Business;

		public void Run()
		{
			foreach (var group in Generator.ItemGroups)
			{
				foreach (var item in group.Items)
				{
					GenerateEntity(group, item);
					GenerateFilter(group, item);
				}
			}
		}

		private void GenerateFilter(ItemGroup group, Item item)
		{
			string path = Path.Combine(Config.EntrityPath, group.Name, "Filter");
			string file = Path.Combine(path, $"{item.Name}Filter.cs");

			if (File.Exists(file))
				return;

			var nameSpace = new List<string> { "System", "XCommon.Patterns.Repository.Entity" };

			StringBuilderIndented builder = new StringBuilderIndented();

			builder
				.ClassInit($"{item.Name}Filter", "FilterBase", $"{Config.EntrityNameSpace}.{group.Name}.Filter", ClassVisility.Public, nameSpace.ToArray())
				.ClassEnd();

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			File.WriteAllText(file, builder.ToString(), Encoding.UTF8);
		}

		private void GenerateEntity(ItemGroup group, Item item)
		{
			string path = Path.Combine(Config.EntrityPath, group.Name, "Auto");
			string file = Path.Combine(path, $"{item.Name}Entity.cs");

			var nameSpace = new List<string> { "System", "XCommon.Patterns.Repository.Entity", "XCommon.Util" };
			nameSpace.AddRange(item.Properties.Select(c => c.NameGroup));

			StringBuilderIndented builder = new StringBuilderIndented();

			builder
				.GenerateFileMessage()
				.ClassInit($"{item.Name}Entity", "EntityBase", $"{Config.EntrityNameSpace}.{group.Name}", ClassVisility.Public, true, nameSpace.ToArray());

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

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			File.WriteAllText(file, builder.ToString(), Encoding.UTF8);
		}
	}
}
