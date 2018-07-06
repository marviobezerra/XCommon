using System.Collections.Generic;
using System.IO;
using System.Linq;
using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.Core.DataBase;
using XCommon.CodeGenerator.Core.Extensions;
using XCommon.CodeGenerator.CSharp.Implementation.Helper;
using XCommon.Util;

namespace XCommon.CodeGenerator.CSharp.Implementation
{
	public class CSharpEntityWriter : BaseWriter, ICSharpEntityWriter
	{
		public void WriteEntity(DataBaseTable item)
		{
			var path = Path.Combine(Config.CSharp.Entity.Path, item.Schema, "Auto");
			var file = $"{item.Name}Entity.cs";

			var nameSpace = new List<string> { "System", "XCommon.Patterns.Repository.Entity", "System.Runtime.Serialization" };
			nameSpace.AddRange(item.Columns.Where(c => c.Schema != item.Schema).Select(c => c.Schema));
			nameSpace.AddRange(item.ProcessRemapSchema(Config));

			var builder = new StringBuilderIndented();

			builder
				.GenerateFileMessage()
				.ClassInit($"{item.Name}Entity", "EntityBase", $"{Config.CSharp.Entity.NameSpace}.{item.Schema}", ClassVisibility.Public, true, nameSpace.ToArray());

			foreach (var column in item.Columns)
			{
				var propertyType = column.ProcessRemapProperty(Config);

				builder
					.AppendLine($"public {propertyType} {column.Name} {{ get; set; }}")
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
				.AppendLine($"return {item.PKName};")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine("set")
				.AppendLine("{")
				.IncrementIndent()
				.AppendLine($"{item.PKName} = value;")
				.DecrementIndent()
				.AppendLine("}")
				.DecrementIndent()
				.AppendLine("}")
				.ClassEnd();

			Writer.WriteFile(path, file, builder, true);
		}

		public void WriteFilter(DataBaseTable item)
		{
			var path = Path.Combine(Config.CSharp.Entity.Path, item.Schema, "Filter");
			var file = $"{item.Name}Filter.cs";
			
			var nameSpace = new List<string> { "System", "XCommon.Patterns.Repository.Entity" };

			var builder = new StringBuilderIndented();

			builder
				.ClassInit($"{item.Name}Filter", "FilterBase", $"{Config.CSharp.Entity.NameSpace}.{item.Schema}.Filter", ClassVisibility.Public, nameSpace.ToArray())
				.ClassEnd();

			Writer.WriteFile(path, file, builder, false);
		}
	}
}
