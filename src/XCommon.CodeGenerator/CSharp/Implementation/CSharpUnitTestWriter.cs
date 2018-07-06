using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.Core.DataBase;
using XCommon.CodeGenerator.Core.Extensions;
using XCommon.Util;

namespace XCommon.CodeGenerator.CSharp.Implementation
{
	public class CSharpUnitTestWritter : BaseWriter, ICSharpUnitTestWriter
	{
		public void WriteTests(DataBaseTable table)
		{
			WriteUnitTest(table);
			WriteDataSource(table);
		}

		private void WriteUnitTest(DataBaseTable item)
		{
			var path = Path.Combine(Config.CSharp.UnitTest.Path, item.Schema);
			var file = $"{item.Name}Test.cs";
		}

		private void WriteDataSource(DataBaseTable item)
		{
			var path = Path.Combine(Config.CSharp.UnitTest.Path, "DataSource", item.Schema);
			var file = $"{item.Name}DataSource.cs";

			var nameSpace = new List<string> { "System", "System.Collections.Generic", "XCommon.Extensions.Converters", "XCommon.Util" };
			nameSpace.Add($"{Config.CSharp.Entity.NameSpace}.{item.Schema}");
			nameSpace.Add($"{Config.CSharp.Entity.NameSpace}.{item.Schema}.Filter");

			var builder = new StringBuilderIndented();

			builder
				.ClassInit($"{item.Name}DataSource", string.Empty, $"{Config.CSharp.UnitTest.NameSpace}.{item.Schema}", ClassVisibility.PublicStatic, nameSpace.ToArray());

			builder
				.AppendLine($"public static IEnumerable<object[]> {item.Name}EntityDataSource")
				.AppendLine("{")
				.IncrementIndent()
				.AppendLine("get")
				.AppendLine("{")
				.IncrementIndent()
				.AppendLine($"var result = new PairList<Pair<{item.Name}Entity>, bool, string>();");

			builder
				.ClassEnd();

			Writer.WriteFile(path, file, builder, false);
		}
	}
}
