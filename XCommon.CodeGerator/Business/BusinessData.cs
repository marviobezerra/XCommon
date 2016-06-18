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
	internal class BusinessData
    {
		private ConfigDataBase Config => Generator.Configuration.DataBase;
		private List<ItemGroup> ItemsGroup => Generator.ItemGroups;

		public void Run()
		{
			ClearFolder();
			GenerateContext();
			GenerateEntity();
			GenerateEntityMap();
		}

		private void ClearFolder()
		{
			if (!Directory.Exists(Config.DataPath))
				Directory.CreateDirectory(Config.DataPath);
			
			foreach (var directory in Directory.GetDirectories(Config.DataPath, "*", SearchOption.TopDirectoryOnly))
			{
				if (directory.Contains("Properties"))
					continue;

				Directory.Delete(directory, true);
			}

			foreach (var file in Directory.GetFiles(Config.DataPath, "*.cs", SearchOption.AllDirectories))
			{
				if (file.Contains("AssemblyInfo.cs"))
					continue;

				File.Delete(file);
			}
		}

		private void GenerateContext()
		{
			List<string> nameSpaces = new List<string> { "System", "Microsoft.Data.Entity", "XCommon.Patterns.Ioc", "Prospect.MyPetLife.Business.Entity.System" };
			nameSpaces.AddRange(ItemsGroup.Select(c => $"{Config.DataNameSpace}.{c.Name}"));
			nameSpaces.AddRange(ItemsGroup.Select(c => $"{Config.DataNameSpace}.{c.Name}.Map"));

			StringBuilderIndented builder = new StringBuilderIndented();

			builder
				.GenerateFileMessage()
				.ClassInit(Config.ContextName, "DbContext", Config.DataNameSpace, ClassVisility.Public, nameSpaces.ToArray())
				.AppendLine()
				.AppendLine("private IApplicationSettings AppSettings => Kernel.Resolve<IApplicationSettings>();")
				.AppendLine();

			foreach (var item in ItemsGroup.SelectMany(c => c.Items))
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

				foreach (var item in ItemsGroup.SelectMany(c => c.Items))
				{
					builder.AppendLine($"{item.Name}Map.Map(modelBuilder, AppSettings.UnitTest);");
				}
			}

			builder
				.AppendLine("}")
				.ClassEnd();

			File.WriteAllText(Path.Combine(Config.DataPath, $"{Config.ContextName}.cs"), builder.ToString(), Encoding.UTF8);
		}

		private void GenerateEntity()
		{
			foreach (var group in ItemsGroup)
			{
				foreach (var item in group.Items)
				{
					string path = Path.Combine(Config.DataPath, group.Name);
					string file = Path.Combine(path, $"{item.Name}.cs");

					var nameSpace = new List<string> { "System", "System.Collections.Generic" };
					nameSpace.AddRange(item.Relationships.Where(c => c.ItemGroupFK != group.Name).Select(c => $"{Config.DataNameSpace}.{c.ItemGroupFK}").Distinct());
					nameSpace.AddRange(item.Relationships.Where(c => c.ItemGroupPK != group.Name).Select(c => $"{Config.DataNameSpace}.{c.ItemGroupPK}").Distinct());
					nameSpace.AddRange(item.Properties.Select(c => c.NameGroup));

					StringBuilderIndented builder = new StringBuilderIndented();

					builder
						.GenerateFileMessage()
						.ClassInit(item.Name, null, $"{Config.DataNameSpace}.{group.Name}", ClassVisility.Public, nameSpace.ToArray());

					foreach (var property in item.Properties)
					{
						builder
							.AppendLine($"public {property.Type} {property.Name} {{ get; set; }}")
							.AppendLine();
					}

					foreach (var relationShip in item.Relationships.Where(c => c.RelationshipType == ItemRelationshipType.Single))
					{
						string relationShipName = ProcessRelationShipName(relationShip, relationShip.ItemPK);

						builder
							.AppendLine($"public virtual {relationShip.ItemPK} {relationShipName} {{ get; set; }}");
					}

					foreach (var relationShip in item.Relationships.Where(c => c.RelationshipType == ItemRelationshipType.Many))
					{
						string relationShipName = ProcessRelationShipName(relationShip, relationShip.ItemFK);

						builder
							.AppendLine($"public virtual ICollection<{relationShip.ItemFK}> {relationShipName} {{ get; set; }}");
					}

					builder
						.ClassEnd();

					if (!Directory.Exists(path))
						Directory.CreateDirectory(path);

					File.WriteAllText(file, builder.ToString(), Encoding.UTF8);
				}
			}
		}

		private void GenerateEntityMap()
		{
			foreach (var group in ItemsGroup)
			{
				foreach (var item in group.Items)
				{
					string path = Path.Combine(Config.DataPath, group.Name, "Map");
					string file = Path.Combine(path, $"{item.Name}Map.cs");

					StringBuilderIndented builder = new StringBuilderIndented();

					builder
						.GenerateFileMessage()
						.ClassInit($"{item.Name}Map", null, $"{Config.DataNameSpace}.{group.Name}.Map", ClassVisility.Internal, "System", "Microsoft.Data.Entity")
						.AppendLine("internal static void Map(ModelBuilder modelBuilder, bool unitTest)")
						.AppendLine("{")
						// = 0 + 1 = 1
						.IncrementIndent()
						.AppendLine($"modelBuilder.Entity<{item.Name}>(entity =>")
						.AppendLine("{")
						// = 1 + 1 = 2
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
					ProcessMapRelationShips(builder, item);

					builder
						// = 2 - 1 = 1
						.DecrementIndent()
						.AppendLine("});")
						// = 1 - 1 = 0
						.DecrementIndent()
						.AppendLine("}")
						.ClassEnd();


					if (!Directory.Exists(path))
						Directory.CreateDirectory(path);

					File.WriteAllText(file, builder.ToString(), Encoding.UTF8);
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

		private void ProcessMapRelationShips(StringBuilderIndented builder, Item item)
		{
			foreach (var relationShip in item.Relationships.Where(c => c.RelationshipType == ItemRelationshipType.Single))
			{
				string relationShipNamePK = ProcessRelationShipName(relationShip, relationShip.ItemGroupPK);
				string relationShipNameFK = ProcessRelationShipName(relationShip, relationShip.ItemGroupFK);

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

		private string ProcessRelationShipName(ItemRelationship relationShip, string defaultName)
		{
			if (Config.Rewrite == null)
				return defaultName;

			var rewrite = Config.Rewrite.FirstOrDefault(c =>
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
