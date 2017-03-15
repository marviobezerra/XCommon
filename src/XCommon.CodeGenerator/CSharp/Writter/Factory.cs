using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XCommon.CodeGenerator.Core.Util;
using XCommon.CodeGenerator.CSharp.Configuration;
using XCommon.CodeGenerator.CSharp.Extensions;
using XCommon.Util;

namespace XCommon.CodeGenerator.CSharp.Writter
{
	internal class Factory : FileWriter
	{
		public void Run(CSharpConfig config)
		{
			GenerateFactoryCuston(config);
			GenerateFactory(config);

			Console.WriteLine("Generate factory code - OK");
		}

		private void GenerateFactoryCuston(CSharpConfig config)
		{
			string path = Path.Combine(config.FactoryPath);
			string file = $"Register.cs";

			if (File.Exists(Path.Combine(path, file)))
			{
				return;
			}

			var nameSpace = new List<string> { "XCommon.Patterns.Ioc" };

			StringBuilderIndented builder = new StringBuilderIndented();

			builder
				.ClassInit("Register", null, $"{config.FacotryNameSpace}", ClassVisility.Public, true, nameSpace.ToArray())
				.AppendLine("public static void RegisterCustom(bool unitTest)")
				.AppendLine("{")
				.AppendLine("}")
				.ClassEnd();

			WriteFile(path, file, builder);
		}

		private void GenerateFactory(CSharpConfig config)
		{
			string path = Path.Combine(config.FactoryPath);
			string file = $"Register.Auto.cs";

			var nameSpace = new List<string> { "XCommon.Patterns.Ioc", "XCommon.Patterns.Specification.Validation", "XCommon.Patterns.Specification.Query" };

			config.DataBaseItems.ForEach(group =>
			{
				nameSpace.Add($"{config.ContractNameSpace}.{group.Name}");
				nameSpace.Add($"{config.ConcreteNameSpace}.{group.Name}");
				nameSpace.Add($"{config.ConcreteNameSpace}.{group.Name}.Query");
				nameSpace.Add($"{config.ConcreteNameSpace}.{group.Name}.Validate");
				nameSpace.Add($"{config.EntrityNameSpace}.{group.Name}");
				nameSpace.Add($"{config.EntrityNameSpace}.{group.Name}.Filter");
				nameSpace.Add($"{config.DataBase.NameSpace}.{group.Name}");
			});

			StringBuilderIndented builder = new StringBuilderIndented();

			builder
				.ClassInit("Register", null, $"{config.FacotryNameSpace}", ClassVisility.Public, true, nameSpace.ToArray())
				.AppendLine("public static void Do(bool unitTest = false)")
				.AppendLine("{")
				.IncrementIndent()
				.AppendLine("RegisterRepository();")
				.AppendLine("RegisterValidate();")
				.AppendLine("RegisterQuery();")
				.AppendLine("RegisterCustom(unitTest);")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine();

			GenerateFactoryRepository(config, builder);
			GenerateFactoryQuery(config, builder);
			GenerateFactoryValidate(config, builder);

			builder
				.ClassEnd();

			WriteFile(path, file, builder);
		}

		private void GenerateFactoryValidate(CSharpConfig config, StringBuilderIndented builder)
		{
			builder
				.AppendLine("private static void RegisterValidate()")
				.AppendLine("{")
				.IncrementIndent();

			foreach (var table in config.DataBaseItems.SelectMany(c => c.Items))
			{
				builder
					.AppendLine($"Kernel.Map<ISpecificationValidation<{table.Name}Entity>>().To<{table.Name}Validate>();");
			}

			builder
				.DecrementIndent()
				.AppendLine("}");
		}

		private void GenerateFactoryQuery(CSharpConfig config, StringBuilderIndented builder)
		{
			builder
				.AppendLine("private static void RegisterQuery()")
				.AppendLine("{")
				.IncrementIndent();

			foreach (var table in config.DataBaseItems.SelectMany(c => c.Items))
			{
				builder
					.AppendLine($"Kernel.Map<ISpecificationQuery<{table.Name}, {table.Name}Filter>>().To<{table.Name}Query>();");
			}

			builder
				.DecrementIndent()
				.AppendLine("}");
		}

		private void GenerateFactoryRepository(CSharpConfig config, StringBuilderIndented builder)
		{
			builder
				.AppendLine("private static void RegisterRepository()")
				.AppendLine("{")
				.IncrementIndent();

			foreach (var table in config.DataBaseItems.SelectMany(c => c.Items))
			{
				builder
					.AppendLine($"Kernel.Map<I{table.Name}Business>().To<{table.Name}Business>();");
			}

			builder
				.DecrementIndent()
				.AppendLine("}");
		}
	}
}
