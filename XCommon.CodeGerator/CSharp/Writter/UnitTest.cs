using System;
using System.Collections.Generic;
using System.IO;
using XCommon.CodeGerator.Core.Entity;
using XCommon.CodeGerator.Core.Util;
using XCommon.CodeGerator.CSharp.Configuration;
using XCommon.CodeGerator.CSharp.Extensions;
using XCommon.Util;

namespace XCommon.CodeGerator.CSharp.Writter
{
	internal class UnitTest : FileWriter
	{
		public void Run(CSharpConfig config)
		{
			foreach (var group in config.DataBaseItems)
			{
				foreach (var item in group.Items)
				{
					GenerateValidateDataSource(config, group, item);
					GenerateValidateTest(config, group, item);
				}
			}

			Console.WriteLine("Generate unit test code - OK");
		}

		private void GenerateValidateTest(CSharpConfig config, ItemGroup group, Item item)
		{
			string path = Path.Combine(config.UnitTestPath, group.Name);
			string file = $"{item.Name}ValidateTest.cs";

			if (File.Exists(Path.Combine(path, file)))
				return;

			var className = $"{item.Name}ValidateTest";
			var nameSpace = new List<string> { "FluentAssertions", "Xunit", "XCommon.Patterns.Ioc", "XCommon.Patterns.Repository.Executes", "XCommon.Patterns.Specification.Validation" };
			nameSpace.Add($"{config.EntrityNameSpace}.{group.Name}");
			nameSpace.Add($"{config.UnitTestNameSpace}.{group.Name}.DataSource");

			StringBuilderIndented builder = new StringBuilderIndented();

			builder
				.ClassInit(className, "BaseTest", $"{config.UnitTestNameSpace}.{group.Name}", ClassVisility.Public, false, nameSpace.ToArray());

			builder
				.AppendLine("[Inject]")
				.AppendLine($"protected ISpecificationValidation<{item.Name}Entity> Specification {{ get; set; }}")
				.AppendLine()
				.AppendLine($"[Theory(DisplayName = \"{item.Name}Validate\")]")
				.AppendLine($"[MemberData(nameof({item.Name}DataSource.DataSource), MemberType = typeof({item.Name}DataSource))]")
				.AppendLine($"public void Validate{item.Name}({item.Name}Entity data, bool valid, string message)")
				.AppendLine("{")
				.IncrementIndent()
				.AppendLine("Execute execute = new Execute();")
				.AppendLine("bool result = Specification.IsSatisfiedBy(data, execute);")
				.AppendLine()
				.AppendLine("result.Should().Be(valid, message);")
				.AppendLine("valid.Should().Be(!execute.HasErro, message);")
				.DecrementIndent()
				.AppendLine("}")
				.ClassEnd();

			WriteFile(path, file, builder);
		}

		private void GenerateValidateDataSource(CSharpConfig config, ItemGroup group, Item item)
		{
			string path = Path.Combine(config.UnitTestPath, group.Name, "DataSource");
			string file = $"{item.Name}DataSource.cs";

			if (File.Exists(Path.Combine(path, file)))
				return;

			var className = $"{item.Name}DataSource";
			var nameSpace = new List<string> { "System", "XCommon.UnitTest", "System.Collections.Generic" };
			nameSpace.Add($"{config.EntrityNameSpace}.{group.Name}");

			StringBuilderIndented builder = new StringBuilderIndented();

			builder
				.ClassInit(className, $"DataSourceBase<{className}, {item.Name}Entity>", $"{config.UnitTestNameSpace}.{group.Name}.DataSource", ClassVisility.Public, false, nameSpace.ToArray());

			builder
				.AppendLine($"protected override List<DataItem<{item.Name}Entity>> Load()")
				.AppendLine("{")
				.IncrementIndent()
				.AppendLine($"List<DataItem<{item.Name}Entity>> result = new List<DataItem<{item.Name}Entity>>();")
				.AppendLine()
				.AppendLine($"result.Add(new DataItem<{item.Name}Entity>")
				.AppendLine("{")
				.IncrementIndent()
				.AppendLine("Item = null,")
				.AppendLine("Message = \"Null value\",")
				.AppendLine("Valid = false")
				.DecrementIndent()
				.AppendLine("});")
				.AppendLine()
				.AppendLine("return result;")
				.DecrementIndent()
				.AppendLine("}")
				.ClassEnd();

			WriteFile(path, file, builder);
		}
	}
}
