using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using XCommon.CodeGenerator.Core.Util;
using XCommon.CodeGenerator.CSharp.Configuration;
using XCommon.Util;
using XCommon.CodeGenerator.CSharp.Extensions;
using System;

namespace XCommon.CodeGenerator.CSharp.Writter
{
	internal class Concrete : FileWriter
	{
		internal void Run(CSharpConfig config)
		{
			GenerateConcrete(config);
			GenerateValidation(config);
			GenerateQuery(config);

			Console.WriteLine("Generate concrect code - OK");
			Console.WriteLine("Generate concrect code [validation] - OK");
			Console.WriteLine("Generate concrect code [query] - OK");
		}

		private void GenerateValidation(CSharpConfig config)
		{
			foreach (var group in config.DataBaseItems)
			{
				foreach (var item in group.Items)
				{
					string path = Path.Combine(config.ConcretePath, group.Name, "Validate");
					string file = $"{item.Name}Validate.cs";

					if (File.Exists(Path.Combine(path, file)))
					{
						continue;
					}

					var nameSpace = new List<string> { "System", "XCommon.Application.Executes", "XCommon.Patterns.Specification.Validation", "XCommon.Patterns.Specification.Validation.Extensions" };
					nameSpace.Add($"{config.EntrityNameSpace}.{group.Name}");

					StringBuilderIndented builder = new StringBuilderIndented();

					builder
						.ClassInit($"{item.Name}Validate", $"SpecificationValidation<{item.Name}Entity>", $"{config.ConcreteNameSpace}.{group.Name}.Validate", ClassVisility.Public, nameSpace.ToArray())						
						.AppendLine($"public override bool IsSatisfiedBy({item.Name}Entity entity, Execute execute)")
						.AppendLine("{")
						.IncrementIndent()
                        .AppendLine("var spefications = NewSpecificationList()")
                        .IncrementIndent()
                        .AppendLine(".AndIsValid(e => e.Key != Guid.Empty, \"Default key isn't valid\");")
                        .DecrementIndent()
                        .AppendLine()
						.AppendLine("return CheckSpecifications(spefications, entity, execute);")
						.DecrementIndent()
						.AppendLine("}")
						.InterfaceEnd();

					WriteFile(path, file, builder);
				}
			}
		}

		private void GenerateQuery(CSharpConfig config)
		{
			foreach (var group in config.DataBaseItems)
			{
				foreach (var table in group.Items)
				{
					string path = Path.Combine(config.ConcretePath, group.Name, "Query");
					string file = Path.Combine(path, $"{table.Name}Query.cs");

					if (File.Exists(file))
					{
						continue;
					}

					var nameSpace = new List<string> { "System", "System.Linq", "System.Collections.Generic", "XCommon.Patterns.Specification.Query", "XCommon.Patterns.Specification.Query.Extensions", "XCommon.Extensions.String", "XCommon.Extensions.Checks" };
					nameSpace.Add($"{config.DataBase.NameSpace}.{group.Name}");
					nameSpace.Add($"{config.EntrityNameSpace}.{group.Name}.Filter");

					StringBuilderIndented builder = new StringBuilderIndented();

					var columnOrder = table.Properties.Any(c => c.Name == "Name")
						? "Name"
						: table.NameKey;

					builder
						.ClassInit($"{table.Name}Query", $"SpecificationQuery<{table.Name}, {table.Name}Filter>", $"{config.ConcreteNameSpace}.{group.Name}.Query", ClassVisility.Public, nameSpace.ToArray())
						.AppendLine($"public override IQueryable<{table.Name}> Build(IQueryable<{table.Name}> source, {table.Name}Filter filter)")
						.AppendLine("{")
						.IncrementIndent()
						.AppendLine($"var spefications = NewSpecificationList()")
						.IncrementIndent()
						.AppendLine($".And(e => e.{table.NameKey} == filter.Key, f => f.Key.HasValue)")
						.AppendLine($".And(e => filter.Keys.Contains(e.{table.NameKey}), f => f.Keys.IsValidList())")
						.AppendLine($".OrderBy(e => e.{columnOrder})")
						.AppendLine(".Take(filter.PageNumber, filter.PageSize);")
						.DecrementIndent()
						.AppendLine()
						.AppendLine("return CheckSpecifications(spefications, source, filter);")
						.DecrementIndent()
						.AppendLine("}")
						.ClassEnd();

					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
					}

					File.WriteAllText(file, builder.ToString(), Encoding.UTF8);
				}
			}
		}

		private void GenerateConcrete(CSharpConfig config)
		{
			foreach (var group in config.DataBaseItems)
			{
				foreach (var item in group.Items)
				{
					string path = Path.Combine(config.ConcretePath, group.Name);
					string file = Path.Combine(path, $"{item.Name}Business.cs");

					if (File.Exists(file))
					{
						continue;
					}

					var nameSpace = new List<string> { "System", " XCommon.Patterns.Repository" };
					nameSpace.Add($"{config.EntrityNameSpace}.{group.Name}");
					nameSpace.Add($"{config.EntrityNameSpace}.{group.Name}.Filter");
					nameSpace.Add($"{config.ContractNameSpace}.{group.Name}");
					nameSpace.Add($"{config.DataBase.NameSpace}");
					nameSpace.Add($"{config.DataBase.NameSpace}.{group.Name}");

					StringBuilderIndented builder = new StringBuilderIndented();

					builder
						.ClassInit($"{item.Name}Business", $"RepositoryEFBase<{item.Name}Entity, {item.Name}Filter, {item.Name}, {config.DataBase.ContextName}>, I{item.Name}Business", $"{config.ConcreteNameSpace}.{group.Name}", ClassVisility.Public, nameSpace.ToArray())
						.ClassEnd();

					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
					}

					File.WriteAllText(file, builder.ToString(), Encoding.UTF8);
				}
			}
		}
	}
}
