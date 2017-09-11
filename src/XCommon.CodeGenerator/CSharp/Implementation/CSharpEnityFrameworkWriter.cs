using System.Collections.Generic;
using System.IO;
using System.Linq;
using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.Core.DataBase;
using XCommon.CodeGenerator.Core.Extensions;
using XCommon.Extensions.String;
using XCommon.Util;

namespace XCommon.CodeGenerator.CSharp.Implementation
{
	public class CSharpEnityFrameworkWriter : BaseWriter, ICSharpEnityFrameworkWriter
	{
		public void WriteContext()
		{
			CleanFolder();

			var path = Config.CSharp.EntityFramework.Path;
			var file = $"{Config.CSharp.EntityFramework.ContextName}.cs";

			var nameSpaces = new List<string> { "System", "Microsoft.EntityFrameworkCore", "Microsoft.EntityFrameworkCore.Infrastructure", "XCommon.Patterns.Ioc", "XCommon.Application" };

			nameSpaces.AddRange(Config.DataBaseItems.Select(c => $"{Config.CSharp.EntityFramework.NameSpace}.{c.Name}"));
			nameSpaces.AddRange(Config.DataBaseItems.Select(c => $"{Config.CSharp.EntityFramework.NameSpace}.{c.Name}.Map"));

			var builder = new StringBuilderIndented();

			builder
				.GenerateFileMessage()
				.ClassInit(Config.CSharp.EntityFramework.ContextName, "DbContext", Config.CSharp.EntityFramework.NameSpace, ClassVisility.Public, nameSpaces.ToArray())
				.AppendLine()
				.AppendLine("private IApplicationSettings AppSettings => Kernel.Resolve<IApplicationSettings>();")
				.AppendLine();

			foreach (var item in Config.DataBaseItems.SelectMany(c => c.Tables))
			{
				builder
					.AppendLine($"public DbSet<{item.Name}> {item.Name} {{ get; set; }}")
					.AppendLine();
			}

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
				.AppendLine()
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

			builder
				.AppendLine("}")
				.ClassEnd();

			Writer.WriteFile(path, file, builder, true);

		}

		public void WriteEntity(DataBaseTable item)
		{
			var path = Path.Combine(Config.CSharp.EntityFramework.Path, item.Schema);
			var file = $"{item.Name}.cs";

			var nameSpace = new List<string> { "System", "System.Collections.Generic" };
			nameSpace.AddRange(item.RelationShips.Where(c => c.TableFK != item.Schema).Select(c => $"{Config.CSharp.EntityFramework.NameSpace}.{c.TableFK}").Distinct());
			nameSpace.AddRange(item.RelationShips.Where(c => c.TablePK != item.Schema).Select(c => $"{Config.CSharp.EntityFramework.NameSpace}.{c.TablePK}").Distinct());
			nameSpace.AddRange(item.Columns.Where(c => c.Schema != item.Schema).Select(c => c.Schema));

			var builder = new StringBuilderIndented();

			builder
				.GenerateFileMessage()
				.ClassInit(item.Name, null, $"{Config.CSharp.EntityFramework.NameSpace}.{item.Schema}", ClassVisility.Public, nameSpace.ToArray());

			foreach (var property in item.Columns)
			{
				builder
					.AppendLine($"public {property.Type} {property.Name} {{ get; set; }}")
					.AppendLine();
			}

			foreach (var relationShip in item.RelationShips.Where(c => c.Type == DataBaseRelationShipType.Single))
			{
				var relationShipName = ProcessRelationShipName(relationShip, relationShip.TablePK);

				builder
					.AppendLine($"public virtual {relationShip.TablePK} {relationShipName} {{ get; set; }}")
					.AppendLine();
			}

			foreach (var relationShip in item.RelationShips.Where(c => c.Type == DataBaseRelationShipType.Many))
			{
				var relationShipName = ProcessRelationShipName(relationShip, relationShip.TableFK);

				builder
					.AppendLine($"public virtual ICollection<{relationShip.TableFK}> {relationShipName} {{ get; set; }}")
					.AppendLine();
			}

			builder
				.ClassEnd();

			Writer.WriteFile(path, file, builder, true);
		}

		public void WriteEntityMap(DataBaseTable item)
		{
			var path = Path.Combine(Config.CSharp.EntityFramework.Path, item.Schema, "Map");
			var file = $"{item.Name}Map.cs";

			var builder = new StringBuilderIndented();

			builder
				.GenerateFileMessage()
				.ClassInit($"{item.Name}Map", null, $"{Config.CSharp.EntityFramework.NameSpace}.{item.Schema}.Map", ClassVisility.Internal, "System", "Microsoft.EntityFrameworkCore")
				.AppendLine("internal static void Map(ModelBuilder modelBuilder, bool unitTest)")
				.AppendLine("{")
				.IncrementIndent()
				.AppendLine($"modelBuilder.Entity<{item.Name}>(entity =>")
				.AppendLine("{")
				.IncrementIndent()
				.AppendLine($"entity.HasKey(e => e.{item.PKName});")
				.AppendLine()
				.AppendLine("if (!unitTest)")
				.IncrementIndent()
				.AppendLine($"entity.ToTable(\"{item.Name}\", \"{item.Schema}\");")
				.DecrementIndent()
				.AppendLine("else")
				.IncrementIndent()
				.AppendLine($"entity.ToTable(\"{item.Schema}{item.Name}\");")
				.DecrementIndent()
				.AppendLine();

			ProcessMapColumns(builder, item);
			ProcessMapRelationShips(builder, item);

			builder
				.DecrementIndent()
				.AppendLine("});")
				.DecrementIndent()
				.AppendLine("}")
				.ClassEnd();

			Writer.WriteFile(path, file, builder, true);
		}

		private void CleanFolder()
		{
			if (!Directory.Exists(Config.CSharp.EntityFramework.Path))
			{
				Directory.CreateDirectory(Config.CSharp.EntityFramework.Path);
			}

			foreach (var directory in Directory.GetDirectories(Config.CSharp.EntityFramework.Path, "*", SearchOption.TopDirectoryOnly))
			{
				if (directory.Contains("Properties"))
				{
					continue;
				}

				Directory.Delete(directory, true);
			}

			foreach (var file in Directory.GetFiles(Config.CSharp.EntityFramework.Path, "*.cs", SearchOption.AllDirectories))
			{
				if (file.Contains("AssemblyInfo.cs"))
				{
					continue;
				}

				File.Delete(file);
			}
		}

		private string ProcessRelationShipName(DataBaseRelationShip relationShip, string defaultName)
		{
			if (Config.CSharp.EntityFramework.Rewrite == null)
			{
				return defaultName;
			}

			var rewrite = Config.CSharp.EntityFramework.Rewrite.FirstOrDefault(c =>
					c.SchemaPK == relationShip.SchemaPK
					&& c.TablePK == relationShip.TablePK
					&& c.ColumnPK == relationShip.ColumnPK
					&& c.SchemaFK == relationShip.SchemaFK
					&& c.TableFK == relationShip.TableFK
					&& c.ColumnFK == relationShip.ColumnFK);

			if (rewrite != null)
			{
				return rewrite.CustomName;
			}

			return defaultName;
		}

		private void ProcessMapColumns(StringBuilderIndented builder, DataBaseTable item)
		{
			foreach (var property in item.Columns)
			{
				builder
					.Append($"entity.Property(e => e.{property.Name})");

				using (builder.Indent())
				{
					if (!property.Nullable)
					{
						builder
							.AppendLine()
							.Append(".IsRequired()");
					}

					if (property.Type == "string" && property.Size.IsEmpty() && property.Size != "MAX")
					{
						builder
							.AppendLine()
							.Append($".HasMaxLength({property.Size})");
					}

					if (property.PK)
					{
						builder
							.AppendLine()
							.Append(".ValueGeneratedNever()");
					}
				}

				builder
					.Append(";")
					.AppendLine()
					.AppendLine();

			}
		}

		private void ProcessMapRelationShips(StringBuilderIndented builder, DataBaseTable item)
		{
			foreach (var relationShip in item.RelationShips.Where(c => c.Type == DataBaseRelationShipType.Single))
			{
				var relationShipNamePK = ProcessRelationShipName(relationShip, relationShip.TablePK);
				var relationShipNameFK = ProcessRelationShipName(relationShip, relationShip.TableFK);

				builder
					.AppendLine("entity")
					.IncrementIndent()
					.AppendLine($".HasOne(d => d.{relationShipNamePK})")
					.AppendLine($".WithMany(p => p.{relationShipNameFK})")
					.AppendLine($".HasForeignKey(d => d.{relationShip.ColumnFK});")
					.DecrementIndent()
					.AppendLine();
			}
		}
	}
}
