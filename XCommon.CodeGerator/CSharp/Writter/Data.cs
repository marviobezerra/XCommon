using System.Collections.Generic;
using System.IO;
using System.Linq;
using XCommon.CodeGerator.Core.Entity;
using XCommon.CodeGerator.Core.Util;
using XCommon.CodeGerator.CSharp.Configuration;
using XCommon.CodeGerator.CSharp.Extensions;
using XCommon.Util;

namespace XCommon.CodeGerator.CSharp.Writter
{
	internal class Data : FileWriter
	{
		public void Run(CSharpConfig config)
		{
			ClearFolder(config);
			GenerateContext(config);
			GenerateEntity(config);
			GenerateEntityMap(config);
		}

		private void ClearFolder(CSharpConfig config)
		{
			if (!Directory.Exists(config.DataBase.Path))
				Directory.CreateDirectory(config.DataBase.Path);
			
			foreach (var directory in Directory.GetDirectories(config.DataBase.Path, "*", SearchOption.TopDirectoryOnly))
			{
				if (directory.Contains("Properties"))
					continue;

				Directory.Delete(directory, true);
			}

			foreach (var file in Directory.GetFiles(config.DataBase.Path, "*.cs", SearchOption.AllDirectories))
			{
				if (file.Contains("AssemblyInfo.cs"))
					continue;

				File.Delete(file);
			}
		}

		private void GenerateContext(CSharpConfig config)
		{
			string path = config.DataBase.Path;
			string file = $"{config.DataBase.ContextName}.cs";

			List<string> nameSpaces = new List<string> { "System", "Microsoft.EntityFrameworkCore", "XCommon.Patterns.Ioc", "XCommon.Application" };
			nameSpaces.AddRange(config.DataBaseItems.Select(c => $"{config.DataBase.NameSpace}.{c.Name}"));
			nameSpaces.AddRange(config.DataBaseItems.Select(c => $"{config.DataBase.NameSpace}.{c.Name}.Map"));

			StringBuilderIndented builder = new StringBuilderIndented();

			builder
				.GenerateFileMessage()
				.ClassInit(config.DataBase.ContextName, "DbContext", config.DataBase.NameSpace, ClassVisility.Public, nameSpaces.ToArray())
				.AppendLine()
				.AppendLine("private IApplicationSettings AppSettings => Kernel.Resolve<IApplicationSettings>();")
				.AppendLine();

			foreach (var item in config.DataBaseItems.SelectMany(c => c.Items))
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
				.AppendLine($"options.UseSqlite(AppSettings.ConnectionString);")
				.AppendLine("return;")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine()
				.AppendLine($"options.UseSqlServer(AppSettings.ConnectionString);")
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

				foreach (var item in config.DataBaseItems.SelectMany(c => c.Items))
				{
					builder.AppendLine($"{item.Name}Map.Map(modelBuilder, AppSettings.UnitTest);");
				}
			}

			builder
				.AppendLine("}")
				.ClassEnd();

			WriteFile(path, file, builder);
		}

		private void GenerateEntity(CSharpConfig config)
		{
			foreach (var group in config.DataBaseItems)
			{
				foreach (var item in group.Items)
				{
					string path = Path.Combine(config.DataBase.Path, group.Name);
					string file = $"{item.Name}.cs";

					var nameSpace = new List<string> { "System", "System.Collections.Generic" };
					nameSpace.AddRange(item.Relationships.Where(c => c.ItemGroupFK != group.Name).Select(c => $"{config.DataBase.NameSpace}.{c.ItemGroupFK}").Distinct());
					nameSpace.AddRange(item.Relationships.Where(c => c.ItemGroupPK != group.Name).Select(c => $"{config.DataBase.NameSpace}.{c.ItemGroupPK}").Distinct());
					nameSpace.AddRange(item.Properties.Where(c => c.NameGroup != group.Name).Select(c => c.NameGroup));

					StringBuilderIndented builder = new StringBuilderIndented();

					builder
						.GenerateFileMessage()
						.ClassInit(item.Name, null, $"{config.DataBase.NameSpace}.{group.Name}", ClassVisility.Public, nameSpace.ToArray());

					foreach (var property in item.Properties)
					{
						builder
							.AppendLine($"public {property.Type} {property.Name} {{ get; set; }}")
							.AppendLine();
					}

					foreach (var relationShip in item.Relationships.Where(c => c.RelationshipType == ItemRelationshipType.Single))
					{
						string relationShipName = ProcessRelationShipName(config, relationShip, relationShip.ItemPK);

						builder
							.AppendLine($"public virtual {relationShip.ItemPK} {relationShipName} {{ get; set; }}")
							.AppendLine();
					}

					foreach (var relationShip in item.Relationships.Where(c => c.RelationshipType == ItemRelationshipType.Many))
					{
						string relationShipName = ProcessRelationShipName(config, relationShip, relationShip.ItemFK);

						builder
							.AppendLine($"public virtual ICollection<{relationShip.ItemFK}> {relationShipName} {{ get; set; }}")
							.AppendLine();
					}

					builder
						.ClassEnd();

					WriteFile(path, file, builder);
				}
			}
		}

		private void GenerateEntityMap(CSharpConfig config)
		{
			foreach (var group in config.DataBaseItems)
			{
				foreach (var item in group.Items)
				{
					string path = Path.Combine(config.DataBase.Path, group.Name, "Map");
					string file = $"{item.Name}Map.cs";

					StringBuilderIndented builder = new StringBuilderIndented();

					builder
						.GenerateFileMessage()
						.ClassInit($"{item.Name}Map", null, $"{config.DataBase.NameSpace}.{group.Name}.Map", ClassVisility.Internal, "System", "Microsoft.EntityFrameworkCore")
						.AppendLine("internal static void Map(ModelBuilder modelBuilder, bool unitTest)")
						.AppendLine("{")
						.IncrementIndent()
						.AppendLine($"modelBuilder.Entity<{item.Name}>(entity =>")
						.AppendLine("{")
						.IncrementIndent()
						.AppendLine($"entity.HasKey(e => e.{item.NameKey});")
						.AppendLine()
						.AppendLine("if (!unitTest)")
						.IncrementIndent()
						.AppendLine($"entity.ToTable(\"{item.Name}\", \"{group.Name}\");")
						.DecrementIndent()
						.AppendLine("else")
						.IncrementIndent()
						.AppendLine($"entity.ToTable(\"{group.Name}{item.Name}\");")
						.DecrementIndent()
						.AppendLine();

					ProcessMapColumns(builder, item);
					ProcessMapRelationShips(config, builder, item);

					builder
						.DecrementIndent()
						.AppendLine("});")
						.DecrementIndent()
						.AppendLine("}")
						.ClassEnd();

					WriteFile(path, file, builder);
				}
			}
		}

		private void ProcessMapColumns(StringBuilderIndented builder, Item item)
		{
			foreach (var property in item.Properties)
			{
				builder
					.Append($"entity.Property(e => e.{property.Name})");

				using (builder.Indent())
				{
					if (!property.Nullable)
						builder
							.AppendLine()
							.Append(".IsRequired()");

					if (property.Type == "string" && !string.IsNullOrEmpty(property.Size) && property.Size != "MAX")
						builder
							.AppendLine()
							.Append($".HasMaxLength({property.Size})");

					if (property.Key)
						builder
							.AppendLine()
							.Append(".ValueGeneratedNever()");
				}

				builder
					.Append(";")
					.AppendLine()
					.AppendLine();

			}
		}

		private void ProcessMapRelationShips(CSharpConfig config, StringBuilderIndented builder, Item item)
		{
			foreach (var relationShip in item.Relationships.Where(c => c.RelationshipType == ItemRelationshipType.Single))
			{
				string relationShipNamePK = ProcessRelationShipName(config, relationShip, relationShip.ItemPK);
				string relationShipNameFK = ProcessRelationShipName(config, relationShip, relationShip.ItemFK);

				builder
					.AppendLine("entity")
					.IncrementIndent()
					.AppendLine($".HasOne(d => d.{relationShipNamePK})")
					.AppendLine($".WithMany(p => p.{relationShipNameFK})")
					.AppendLine($".HasForeignKey(d => d.{relationShip.PropertyFK});")
					.DecrementIndent()
					.AppendLine();
			}
		}

		private string ProcessRelationShipName(CSharpConfig config, ItemRelationship relationShip, string defaultName)
		{
			if (config.DataBase.Rewrite == null)
				return defaultName;

			var rewrite = config.DataBase.Rewrite.FirstOrDefault(c =>
					c.SchemaPK == relationShip.ItemGroupPK
					&& c.TablePK == relationShip.ItemPK
					&& c.ColumnPK == relationShip.PropertyPK
					&& c.SchemaFK == relationShip.ItemGroupFK
					&& c.TableFK == relationShip.ItemFK
					&& c.ColumnFK == relationShip.PropertyFK);

			if (rewrite != null)
				return rewrite.CustonName;

			return defaultName;
		}
	}
}
