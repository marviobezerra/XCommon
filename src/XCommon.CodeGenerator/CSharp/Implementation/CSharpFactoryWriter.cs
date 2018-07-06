using System.Collections.Generic;
using System.IO;
using System.Linq;
using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.Core.Extensions;
using XCommon.Util;

namespace XCommon.CodeGenerator.CSharp.Implementation
{
	public class CSharpFactoryWritter : BaseWriter, ICSharpFactoryWriter
	{
		public void WriteFactoryCustom()
		{
			var path = Path.Combine(Config.CSharp.Factory.Path);
			var file = $"Register.cs";

			if (File.Exists(Path.Combine(path, file)))
			{
				return;
			}

			var nameSpace = new List<string> { "XCommon.Patterns.Ioc" };

			var builder = new StringBuilderIndented();

			builder
				.ClassInit("Register", null, $"{Config.CSharp.Factory.NameSpace}", ClassVisibility.Public, true, nameSpace.ToArray())
				.AppendLine("public static void RegisterCustom(bool production, bool unitTest)")
				.AppendLine("{")
				.AppendLine("}")
				.ClassEnd();

			Writer.WriteFile(path, file, builder, false);
		}

		public void WriteFactory()
		{
			var path = Path.Combine(Config.CSharp.Factory.Path);
			var file = $"Register.Auto.cs";

			var nameSpace = new List<string> { "XCommon.Patterns.Ioc", "XCommon.Patterns.Specification.Validation", "XCommon.Patterns.Specification.Query" };

			foreach (var schema in Config.DataBaseItems)
			{
				if (Config.CSharp.Repository.Contract != null && Config.CSharp.Repository.Contract.Execute)
				{
					nameSpace.Add($"{Config.CSharp.Repository.Contract.NameSpace}.{schema.Name}");
				}

				nameSpace.Add($"{Config.CSharp.Repository.Concrecte.NameSpace}.{schema.Name}");
				nameSpace.Add($"{Config.CSharp.Repository.Concrecte.NameSpace}.{schema.Name}.Query");
				nameSpace.Add($"{Config.CSharp.Repository.Concrecte.NameSpace}.{schema.Name}.Validate");
				nameSpace.Add($"{Config.CSharp.Entity.NameSpace}.{schema.Name}");
				nameSpace.Add($"{Config.CSharp.Entity.NameSpace}.{schema.Name}.Filter");
				nameSpace.Add($"{Config.CSharp.EntityFramework.NameSpace}.{schema.Name}");
			};

			var builder = new StringBuilderIndented();

			builder
				.ClassInit("Register", null, $"{Config.CSharp.Factory.NameSpace}", ClassVisibility.Public, true, nameSpace.ToArray())
				.AppendLine("public static void Do(bool production, bool unitTest)")
				.AppendLine("{")
				.IncrementIndent();

			if (Config.CSharp.Repository.Contract != null && Config.CSharp.Repository.Contract.Execute)
			{
				builder
					.AppendLine("RegisterRepository();");

			}

			builder
				.AppendLine("RegisterValidate();")
				.AppendLine("RegisterQuery();")
				.AppendLine("RegisterCustom(production, unitTest);")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine();

			if (Config.CSharp.Repository.Contract != null && Config.CSharp.Repository.Contract.Execute)
			{
				GenerateFactoryRepository(builder);
			}

			GenerateFactoryQuery(builder);
			GenerateFactoryValidate(builder);

			builder
				.ClassEnd();

			Writer.WriteFile(path, file, builder, true);
		}

		private void GenerateFactoryValidate(StringBuilderIndented builder)
		{
			builder
				.AppendLine("private static void RegisterValidate()")
				.AppendLine("{")
				.IncrementIndent();

			foreach (var table in Config.DataBaseItems.SelectMany(c => c.Tables))
			{
				builder
					.AppendLine($"Kernel.Map<ISpecificationValidation<{table.Name}Entity>>().To<{table.Name}Validate>();");
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

			foreach (var table in Config.DataBaseItems.SelectMany(c => c.Tables))
			{
				builder
					.AppendLine($"Kernel.Map<ISpecificationQuery<{table.Name}, {table.Name}Filter>>().To<{table.Name}Query>();");
			}

			builder
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine();
		}

		private void GenerateFactoryRepository(StringBuilderIndented builder)
		{
			builder
				.AppendLine("private static void RegisterRepository()")
				.AppendLine("{")
				.IncrementIndent();

			foreach (var table in Config.DataBaseItems.SelectMany(c => c.Tables))
			{
				builder
					.AppendLine($"Kernel.Map<I{table.Name}Business>().To<{table.Name}Business>();");
			}

			builder
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine();
		}
	}
}
