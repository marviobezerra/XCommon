using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using XCommon.CodeGerator.Configuration;
using XCommon.CodeGerator.Entity;
using XCommon.CodeGerator.Extensions;
using XCommon.Util;

namespace XCommon.CodeGerator.Business
{
	internal class BusinessFactory
    {
		private ConfigBusiness Config => Generator.Configuration.Business;

		private List<ItemGroup> ItemsGroup => Generator.ItemGroups;
		
		public void Run()
		{
			GenerateFactory();
		}

		private void GenerateFactory()
		{
			string path = Path.Combine(Config.FactoryPath);
			string file = Path.Combine(path, $"Register.Auto.cs");

			var nameSpace = new List<string> { "XCommon.Patterns.Ioc", " XCommon.Patterns.Specification.Entity", "XCommon.Patterns.Specification.Query" };

			ItemsGroup.ForEach(group =>
			{
				nameSpace.Add($"{Config.ContractNameSpace}.{group.Name}");
				nameSpace.Add($"{Config.ConcretNameSpace}.{group.Name}");
				nameSpace.Add($"{Config.ConcretNameSpace}.{group.Name}.Query");
				nameSpace.Add($"{Config.ConcretNameSpace}.{group.Name}.Validate");
				nameSpace.Add($"{Config.EntrityNameSpace}.{group.Name}");
				nameSpace.Add($"{Config.EntrityNameSpace}.{group.Name}.Filter");
				nameSpace.Add($"{Generator.Configuration.DataBase.DataNameSpace}.{group.Name}");
			});

			StringBuilderIndented builder = new StringBuilderIndented();

			builder
				.ClassInit("Register", null, $"{Config.FacotryNameSpace}", ClassVisility.Public, true, nameSpace.ToArray())
				.AppendLine("public static void Do(bool unitTest = false)")
				.AppendLine("{")
				.IncrementIndent()
				.AppendLine("RegisterRepository();")
				.AppendLine("RegisterValidate();")
				.AppendLine("RegisterQuery();")
				.AppendLine("RegisterCuston();")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine();

			GenerateFactoryRepository(builder);
			GenerateFactoryQuery(builder);
			GenerateFactoryValidate(builder);

			builder
				.ClassEnd();

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			File.WriteAllText(file, builder.ToString(), Encoding.UTF8);

		}

		private void GenerateFactoryValidate(StringBuilderIndented builder)
		{
			builder
					.AppendLine("private static void RegisterValidate()")
					.AppendLine("{")
					.IncrementIndent();

			foreach (var table in ItemsGroup.SelectMany(c => c.Items))
			{
				builder
					.AppendLine($"Kernel.Map<ISpecificationEntity<{table.Name}Entity>>().To<{table.Name}Validate>();");
			}

			builder
				.DecrementIndent()
				.AppendLine("}");
		}

		private void GenerateFactoryQuery(StringBuilderIndented builder)
		{
			builder
					.AppendLine("private static void RegisterQuery()")
					.AppendLine("{")
					.IncrementIndent();

			foreach (var table in ItemsGroup.SelectMany(c => c.Items))
			{
				builder
					.AppendLine($"Kernel.Map<IQueryBuilder<{table.Name}, {table.Name}Filter>>().To<{table.Name}Query>();");
			}

			builder
				.DecrementIndent()
				.AppendLine("}");
		}

		private void GenerateFactoryRepository(StringBuilderIndented builder)
		{
			builder
					.AppendLine("private static void RegisterRepository()")
					.AppendLine("{")
					.IncrementIndent();

			foreach (var table in ItemsGroup.SelectMany(c => c.Items))
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
