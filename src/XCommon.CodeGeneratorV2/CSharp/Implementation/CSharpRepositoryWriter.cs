using System.Collections.Generic;
using System.IO;
using System.Linq;
using XCommon.CodeGeneratorV2.Core;
using XCommon.CodeGeneratorV2.Core.DataBase;
using XCommon.CodeGeneratorV2.Core.Extensions;
using XCommon.Util;

namespace XCommon.CodeGeneratorV2.CSharp.Implementation
{
	public class CSharpRepositoryWritter : BaseWriter, ICSharpRepositoryWriter
	{
		public void WriteConcrete(DataBaseTable item)
		{
			var path = Path.Combine(Config.CSharp.Repository.Concrecte.Path, item.Schema);
			var file = $"{item.Name}Business.cs";

			var nameSpace = new List<string> { "System", " XCommon.Patterns.Repository" };
			nameSpace.Add($"{Config.CSharp.Repository.Concrecte.NameSpace}.{item.Schema}");
			nameSpace.Add($"{Config.CSharp.Repository.Concrecte.NameSpace}.{item.Schema}.Filter");
			nameSpace.Add($"{Config.CSharp.Repository.Contract.NameSpace}.{item.Schema}");

			if (Config.CSharp.EntityFramework != null)
			{
				nameSpace.Add($"{Config.CSharp.EntityFramework.NameSpace}");
				nameSpace.Add($"{Config.CSharp.EntityFramework.NameSpace}.{item.Schema}");
			}

			var builder = new StringBuilderIndented();

			builder
				.ClassInit($"{item.Name}Business", $"RepositoryEFBase<{item.Name}Entity, {item.Name}Filter, {item.Name}, {Config.CSharp.EntityFramework.ContextName}>, I{item.Name}Business", $"{Config.CSharp.Repository.Concrecte.NameSpace}.{item.Schema}", ClassVisility.Public, nameSpace.ToArray())
				.ClassEnd();

			Writer.WriteFile(path, file, builder, false);
		}

		public void WriteContract(DataBaseTable item)
		{
			var path = Path.Combine(Config.CSharp.Repository.Contract.Path, item.Schema);
			var file = $"I{item.Name}Business.cs";
			
			var nameSpace = new List<string> { "System", "XCommon.Patterns.Repository" };
			nameSpace.Add($"{Config.CSharp.Entity.NameSpace}.{item.Schema}");
			nameSpace.Add($"{Config.CSharp.Entity.NameSpace}.{item.Schema}.Filter");

			var builder = new StringBuilderIndented();

			builder
				.InterfaceInit($"I{item.Name}Business", $"IRepositoryEF<{item.Name}Entity, {item.Name}Filter>", $"{Config.CSharp.Repository.Contract.NameSpace}.{item.Schema}", ClassVisility.Public, nameSpace.ToArray())
				.InterfaceEnd();

			Writer.WriteFile(path, file, builder, false);
		}

		public void WriteQuery(DataBaseTable item)
		{
			var path = Path.Combine(Config.CSharp.Repository.Concrecte.Path, item.Schema, "Query");
			var file = $"{item.Name}Query.cs";
			
			var nameSpace = new List<string> { "System", "System.Linq", "System.Collections.Generic", "XCommon.Patterns.Specification.Query", "XCommon.Patterns.Specification.Query.Extensions", "XCommon.Extensions.String", "XCommon.Extensions.Checks" };

			if (Config.CSharp.EntityFramework != null)
			{
				nameSpace.Add($"{Config.CSharp.EntityFramework.NameSpace}.{item.Schema}");
			}

			nameSpace.Add($"{Config.CSharp.Entity.NameSpace}.{item.Schema}.Filter");
			
			var builder = new StringBuilderIndented();

			var columnOrder = item.Columns.Any(c => c.Name == "Name")
				? "Name"
				: item.PKName;

			builder
				.ClassInit($"{item.Name}Query", $"SpecificationQuery<{item.Name}, {item.Name}Filter>", $"{Config.CSharp.Repository.Concrecte.NameSpace}.{item.Schema}.Query", ClassVisility.Public, nameSpace.ToArray())
				.AppendLine($"public override IQueryable<{item.Name}> Build(IQueryable<{item.Name}> source, {item.Name}Filter filter)")
				.AppendLine("{")
				.IncrementIndent()
				.AppendLine($"var spefications = NewSpecificationList()")
				.IncrementIndent()
				.AppendLine($".And(e => e.{item.PKName} == filter.Key, f => f.Key.HasValue)")
				.AppendLine($".And(e => filter.Keys.Contains(e.{item.PKName}), f => f.Keys.IsValidList())")
				.AppendLine($".OrderBy(e => e.{columnOrder})")
				.AppendLine(".Take(filter.PageNumber, filter.PageSize);")
				.DecrementIndent()
				.AppendLine()
				.AppendLine("return CheckSpecifications(spefications, source, filter);")
				.DecrementIndent()
				.AppendLine("}")
				.ClassEnd();

			Writer.WriteFile(path, file, builder, false);
		}

		public void WriteValidation(DataBaseTable item)
		{
			var path = Path.Combine(Config.CSharp.Repository.Concrecte.Path, item.Schema, "Validate");
			var file = $"{item.Name}Validate.cs";


			var nameSpace = new List<string> { "System", "XCommon.Application.Executes", "XCommon.Patterns.Specification.Validation", "XCommon.Patterns.Specification.Validation.Extensions" };
			nameSpace.Add($"{Config.CSharp.Entity.NameSpace}.{item.Schema}");

			var builder = new StringBuilderIndented();

			builder
				.ClassInit($"{item.Name}Validate", $"SpecificationValidation<{item.Name}Entity>", $"{Config.CSharp.Repository.Concrecte.NameSpace}.{item.Schema}.Validate", ClassVisility.Public, nameSpace.ToArray())
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

			Writer.WriteFile(path, file, builder, false);
		}
	}
}
