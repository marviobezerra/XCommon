using System;
using System.Collections.Generic;
using System.IO;
using XCommon.CodeGenerator.Core.Entity;
using XCommon.CodeGenerator.Core.Util;
using XCommon.CodeGenerator.CSharp.Configuration;
using XCommon.CodeGenerator.CSharp.Extensions;
using XCommon.Util;

namespace XCommon.CodeGenerator.CSharp.Writter
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
			string file = $"{item.Name}Test.cs";

			if (File.Exists(Path.Combine(path, file)))
				return;

			var className = $"{item.Name}Test";
			var nameSpace = new List<string> { "System.Threading.Tasks", "System.Linq", "FluentAssertions", "Xunit", "XCommon.Patterns.Ioc", "XCommon.Application.Executes", "XCommon.Patterns.Specification.Validation" };
			nameSpace.Add($"{config.ContractNameSpace}.{group.Name}");
			nameSpace.Add($"{config.EntrityNameSpace}.{group.Name}");
            nameSpace.Add($"{config.EntrityNameSpace}.{group.Name}.Filter");
            nameSpace.Add($"{config.UnitTestNameSpace}.{group.Name}.DataSource");

			StringBuilderIndented builder = new StringBuilderIndented();

			builder
				.ClassInit(className, "BaseTest", $"{config.UnitTestNameSpace}.{group.Name}", ClassVisility.Public, false, nameSpace.ToArray());

			builder
				.AppendLine("[Inject]")
				.AppendLine($"protected ISpecificationValidation<{item.Name}Entity> SpecificationValidation {{ get; set; }}")
                .AppendLine()
                .AppendLine("[Inject]")
                .AppendLine($"protected I{item.Name}Business {item.Name}Business {{ get; set; }}")
                .AppendLine()
				.AppendLine($"[Theory(DisplayName = \"{item.Name} (Validate)\")]")
				.AppendLine($"[MemberData(nameof({item.Name}DataSource.EntityValidation), MemberType = typeof({item.Name}DataSource))]")
				.AppendLine($"public void Validate{item.Name}({item.Name}Entity data, bool expected, string message)")
				.AppendLine("{")
				.IncrementIndent()
				.AppendLine("Execute execute = new Execute();")
				.AppendLine("bool result = SpecificationValidation.IsSatisfiedBy(data, execute);")
				.AppendLine()
				.AppendLine("result.Should().Be(expected, message);")
				.AppendLine("expected.Should().Be(!execute.HasErro, message);")
				.DecrementIndent()
				.AppendLine("}")
                .AppendLine()

                .AppendLine($"[Theory(DisplayName = \"{item.Name} (Load)\")]")
                .AppendLine($"[MemberData(nameof({item.Name}DataSource.EntityFilter), MemberType = typeof({item.Name}DataSource))]")
                .AppendLine($"public async Task GetByFilterAsync({item.Name}Filter filter, int expected, string message)")
                .AppendLine("{")
                .IncrementIndent()
                .AppendLine($"//Scenery.Load(SceneryType.{item.Name});")
                .AppendLine($"var result = await {item.Name}Business.GetByFilterAsync(filter);")
                .AppendLine()
                .AppendLine("result.Count().Should().Be(expected, message);")
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
			var nameSpace = new List<string> { "System", "XCommon.Util", "System.Collections.Generic" };
			nameSpace.Add($"{config.EntrityNameSpace}.{group.Name}");
            nameSpace.Add($"{config.EntrityNameSpace}.{group.Name}.Filter");

			StringBuilderIndented builder = new StringBuilderIndented();

			builder
				.ClassInit(className, string.Empty, $"{config.UnitTestNameSpace}.{group.Name}.DataSource", ClassVisility.Public, false, nameSpace.ToArray());

			builder
                .AppendLine($"public static IEnumerable<object[]> EntityValidation")
                .AppendLine("{")
                .IncrementIndent()
                .AppendLine("get")
                .AppendLine("{")
                .IncrementIndent()

                .AppendLine($"PairList<{item.Name}Entity, bool, string> result = new PairList<{item.Name}Entity, bool, string>();")
                .AppendLine()
                .AppendLine("result.Add(null, false, \"Null value\");")
                .AppendLine($"result.Add(new {item.Name}Entity(), false, \"Default value\");")
                .AppendLine()
                .AppendLine("return result.Cast();")
                .DecrementIndent()
                .AppendLine("}")
                .DecrementIndent()
                .AppendLine("}")
                .AppendLine()
                
                .AppendLine($"public static IEnumerable<object[]> EntityFilter")
                .AppendLine("{")
                .IncrementIndent()
                .AppendLine("get")
                .AppendLine("{")
                .IncrementIndent()
                .AppendLine($"PairList<{item.Name}Filter, int, string> result = new PairList<{item.Name}Filter, int, string>();")
                .AppendLine()
                .AppendLine($"result.Add(new {item.Name}Filter(), 0, \"Default filter\");")
                .AppendLine()
                .AppendLine("return result.Cast();")
                .DecrementIndent()
                .AppendLine("}")
                .DecrementIndent()
                .AppendLine("}")

                .ClassEnd();

			WriteFile(path, file, builder);
		}
	}
}
