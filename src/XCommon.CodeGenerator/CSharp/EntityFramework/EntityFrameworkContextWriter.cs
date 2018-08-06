using System.Collections.Generic;
using System.Linq;
using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.Core.Extensions;
using XCommon.Util;

namespace XCommon.CodeGenerator.CSharp.EntityFramework
{
	public class EntityFrameworkContextWriter : BaseWriter
	{
		public override bool Write()
		{
			var path = Config.CSharp.EntityFramework.Path;
			var file = $"{Config.CSharp.EntityFramework.ContextName}.cs";

			var nameSpaces = GetNameSpaces();

			var builder = new StringBuilderIndented();

			builder
				.GenerateFileMessage()
				.ClassInit(Config.CSharp.EntityFramework.ContextName, "DbContext", Config.CSharp.EntityFramework.NameSpace, ClassVisibility.Public, nameSpaces.ToArray())
				.AppendLine();

			WritePrivateProperties(builder);
			WriteDbSetProperties(builder);
			WriteOnConfiguringMethod(builder);
			WriteOnModelCreatingMethod(builder);

			builder
				.AppendLine("}")
				.ClassEnd();

			Writer.WriteFile(path, file, builder, true);
			return true;
		}

		protected virtual void WriteOnModelCreatingMethod(StringBuilderIndented builder)
		{
			builder
				.AppendLine("protected override void OnModelCreating(ModelBuilder modelBuilder)")
				.AppendLine("{");

			using (builder.Indent())
			{
				builder
					.AppendLine();

				foreach (var item in Config.DataBaseItems.SelectMany(c => c.Tables))
				{
					builder.AppendLine($"{item.Name}Map.Map(modelBuilder, AppSettings.UnitTest);");
				}
			}
		}

		protected virtual void WriteOnConfiguringMethod(StringBuilderIndented builder)
		{
			builder
				.AppendLine("protected override void OnConfiguring(DbContextOptionsBuilder options)")
				.AppendLine("{")
				.IncrementIndent()
				.AppendLine("if (AppSettings.UnitTest)")
				.AppendLine("{")
				.IncrementIndent()
				.AppendLine($"options")
				.IncrementIndent()
				.AppendLine($".UseInMemoryDatabase(\"{Config.CSharp.EntityFramework.ContextName}\")")
				.AppendLine(".ConfigureWarnings(config => config.Ignore(InMemoryEventId.TransactionIgnoredWarning));")
				.DecrementIndent()
				.AppendLine()
				.AppendLine("return;")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine()
				.AppendLine($"options.UseSqlServer(AppSettings.DataBaseConnectionString);")
				.AppendLine()
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine();
		}

		protected virtual void WriteDbSetProperties(StringBuilderIndented builder)
		{
			foreach (var item in Config.DataBaseItems.SelectMany(c => c.Tables))
			{
				builder
					.AppendLine($"public DbSet<{item.Name}> {item.Name} {{ get; set; }}")
					.AppendLine();
			}
		}

		protected virtual void WritePrivateProperties(StringBuilderIndented builder)
		{
			builder
				.AppendLine("private IApplicationSettings AppSettings => Kernel.Resolve<IApplicationSettings>();")
				.AppendLine();
		}

		protected virtual List<string> GetNameSpaces()
		{
			var result = new List<string> { "System", "Microsoft.EntityFrameworkCore", "Microsoft.EntityFrameworkCore.Diagnostics", "XCommon.Patterns.Ioc", "XCommon.Application.Settings" };

			result.AddRange(Config.DataBaseItems.Select(c => $"{Config.CSharp.EntityFramework.NameSpace}.{c.Name}"));
			result.AddRange(Config.DataBaseItems.Select(c => $"{Config.CSharp.EntityFramework.NameSpace}.{c.Name}.Map"));

			return result;
		}
	}
}
