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
	internal class BusinessConcret
    {
		private ConfigBusiness Config => Generator.Configuration.Business;

		private List<ItemGroup> ItemsGroup => Generator.ItemGroups;

		public void Run()
		{
			GenerateConcret();
			GenerateValidation();
			GenerateQuery();
		}

		private void GenerateValidation()
		{
			foreach (var group in ItemsGroup)
			{
				foreach (var item in group.Items)
				{
					string path = Path.Combine(Config.ConcretPath, group.Name, "Validate");
					string file = Path.Combine(path, $"{item.Name}Validate.cs");

					if (File.Exists(file))
						continue;

					var nameSpace = new List<string> { "System", "XCommon.Patterns.Repository.Executes", "XCommon.Patterns.Specification.Entity", "XCommon.Patterns.Specification.Entity.Implementation" };
					nameSpace.Add($"{Config.EntrityNameSpace}.{group.Name}");

					StringBuilderIndented builder = new StringBuilderIndented();

					builder
						.ClassInit($"{item.Name}Validate", $"ISpecificationEntity<{item.Name}Entity>", $"{Config.ConcretNameSpace}.{group.Name}.Validate", ClassVisility.Public, nameSpace.ToArray())
						.AppendLine($"public bool IsSatisfiedBy({item.Name}Entity entity)")
						.AppendLine("{")
						.IncrementIndent()
						.AppendLine("return IsSatisfiedBy(entity, new Execute());")
						.DecrementIndent()
						.AppendLine("}")
						.AppendLine()
						.AppendLine($"public bool IsSatisfiedBy({item.Name}Entity entity, Execute execute)")
						.AppendLine("{")
						.IncrementIndent()
						.AppendLine($"SpecificationEntity<{item.Name}Entity> result = new SpecificationEntity<{item.Name}Entity>();")
						.AppendLine("return result.IsSatisfiedBy(entity, execute);")
						.DecrementIndent()
						.AppendLine("}")
						.InterfaceEnd();

					if (!Directory.Exists(path))
						Directory.CreateDirectory(path);

					File.WriteAllText(file, builder.ToString(), Encoding.UTF8);
				}
			}
		}

		private void GenerateQuery()
		{
			foreach (var group in ItemsGroup)
			{
				foreach (var table in group.Items)
				{
					string path = Path.Combine(Config.ConcretPath, group.Name, "Query");
					string file = Path.Combine(path, $"{table.Name}Query.cs");

					if (File.Exists(file))
						continue;

					var nameSpace = new List<string> { "System", "System.Linq", "System.Collections.Generic", "XCommon.Patterns.Specification.Query", "XCommon.Extensions.String" };
					nameSpace.Add($"{Generator.Configuration.DataBase.DataNameSpace}.{group.Name}");
					nameSpace.Add($"{Generator.Configuration.DataBase.DataNameSpace}.{group.Name}.Filter");

					StringBuilderIndented builder = new StringBuilderIndented();

					var columnOrder = table.Properties.Any(c => c.Name == "Name")
						? "Name"
						: table.NameKey;

					builder
						.ClassInit($"{table.Name}Query", $"IQueryBuilder<{table.Name}, {table.Name}Filter>", $"{Config.ConcretNameSpace}.{group.Name}.Query", ClassVisility.Public, nameSpace.ToArray())
						.AppendLine($"public IQueryable<{table.Name}> Build(IQueryable<{table.Name}> query, {table.Name}Filter filter)")
						.AppendLine("{")
						.IncrementIndent()
						.AppendLine($"QueryBuilder<{table.Name}, {table.Name}Filter> result = new QueryBuilder<{table.Name}, {table.Name}Filter>()")
						.IncrementIndent()
						.AppendLine($".And(c => c.{table.NameKey} == filter.Key, c => c.Key.HasValue)")
						.AppendLine($".And(c => filter.Keys.Contains(c.{table.NameKey}), c => c.Keys.Count > 0)")
						.AppendLine($".OrderBy(c => c.{columnOrder})")
						.AppendLine(".Take(filter.PageNumber, filter.PageSize);")
						.DecrementIndent()
						.AppendLine()
						.AppendLine("return result.Build(query, filter);")
						.DecrementIndent()
						.AppendLine("}")
						.AppendLine()
						.AppendLine($"public IQueryable<{table.Name}> Build(IEnumerable<{table.Name}> query, {table.Name}Filter filter)")
						.AppendLine("{")
						.IncrementIndent()
						.AppendLine("return Build(query.AsQueryable(), filter);")
						.DecrementIndent()
						.AppendLine("}")
						.InterfaceEnd();

					if (!Directory.Exists(path))
						Directory.CreateDirectory(path);

					File.WriteAllText(file, builder.ToString(), Encoding.UTF8);
				}
			}
		}

		private void GenerateConcret()
		{
			foreach (var group in ItemsGroup)
			{
				foreach (var item in group.Items)
				{
					string path = Path.Combine(Config.ConcretPath, group.Name);
					string file = Path.Combine(path, $"{item.Name}Business.cs");

					if (File.Exists(file))
						continue;

					var nameSpace = new List<string> { "System", "Prospect.MyPetLife.Business.Concret.Base" };
					nameSpace.Add($"{Config.EntrityNameSpace}.{group.Name}");
					nameSpace.Add($"{Config.EntrityNameSpace}.{group.Name}.Filter");
					nameSpace.Add($"{Config.ContractNameSpace}.{group.Name}");
					nameSpace.Add($"{Generator.Configuration.DataBase.ContextName}");
					nameSpace.Add($"{Generator.Configuration.DataBase.ContextName}.{group.Name}");

					StringBuilderIndented builder = new StringBuilderIndented();

					builder
						.ClassInit($"{item.Name}Business", $"RepositoryBase<{item.Name}Entity, {item.Name}Filter, {item.Name}, {Generator.Configuration.DataBase.ContextName}>, I{item.Name}Business", $"{Config.ConcretNameSpace}.{group.Name}", ClassVisility.Public, nameSpace.ToArray())
						.ClassEnd();

					if (!Directory.Exists(path))
						Directory.CreateDirectory(path);

					File.WriteAllText(file, builder.ToString(), Encoding.UTF8);
				}
			}
		}
	}
}
