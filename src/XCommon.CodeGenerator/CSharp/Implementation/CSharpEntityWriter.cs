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
			nameSpace.AddRange(item.ProcessRemapSchema(Config, false));

			var builder = new StringBuilderIndented();
			var baseClass = "EntityBase";
			var appClass = Config.CSharp.ApplicationClasses.FirstOrDefault(c => c.Name == item.Name && c.Schema == item.Schema);

			if (Config.CSharp.UsingApplicationBase && appClass != null)
			{
				nameSpace = new List<string>();
				baseClass = $"{appClass.NamespaceEntity}.{item.Name}Entity";

				builder
					.GenerateFileMessage()
					.ClassInit($"{item.Name}Entity", baseClass, $"{Config.CSharp.Entity.NameSpace}.{item.Schema}", ClassVisibility.Public, true, nameSpace.ToArray())
					.ClassEnd();
			}
			else
			{
				builder
					.GenerateFileMessage()
					.ClassInit($"{item.Name}Entity", baseClass, $"{Config.CSharp.Entity.NameSpace}.{item.Schema}", ClassVisibility.Public, true, nameSpace.ToArray());

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
			}
			Writer.WriteFile(path, file, builder, true);
		}

		public void WriteFilter(DataBaseTable item)
		{
			var path = Path.Combine(Config.CSharp.Entity.Path, item.Schema, "Filter");
			var file = $"{item.Name}Filter.cs";

			var nameSpace = new List<string> { "System", "XCommon.Patterns.Repository.Entity" };
			var builder = new StringBuilderIndented();

			var baseClass = "FilterBase";
			var appClass = Config.CSharp.ApplicationClasses.FirstOrDefault(c => c.Name == item.Name && c.Schema == item.Schema);

			if (Config.CSharp.UsingApplicationBase && appClass != null)
			{
				nameSpace = new List<string>();
				baseClass = $"{appClass.NamespaceFilter}.{item.Name}Filter";
			}

			builder
				.ClassInit($"{item.Name}Filter", baseClass, $"{Config.CSharp.Entity.NameSpace}.{item.Schema}.Filter", ClassVisibility.Public, nameSpace.ToArray())
				.ClassEnd();

			Writer.WriteFile(path, file, builder, false);
		}
	}
}
