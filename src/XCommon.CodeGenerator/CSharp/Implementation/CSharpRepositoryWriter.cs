using System.Collections.Generic;
using System.IO;
using System.Linq;
using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.Core.DataBase;
using XCommon.CodeGenerator.Core.Extensions;
using XCommon.Util;

namespace XCommon.CodeGenerator.CSharp.Implementation
{
	public class CSharpRepositoryWritter : BaseWriter, ICSharpRepositoryWriter
	{
		public void WriteConcrete(DataBaseTable item)
		{
			var path = Path.Combine(Config.CSharp.Repository.Concrecte.Path, item.Schema);
			var file = $"{item.Name}Business.cs";

			var parentClass = $"RepositoryEFBase<{item.Name}Entity, {item.Name}Filter, {item.Name}, {Config.CSharp.EntityFramework.ContextName}>";
			var nameSpace = new List<string> { "System", " XCommon.Patterns.Repository" };
			nameSpace.Add($"{Config.CSharp.Entity.NameSpace}.{item.Schema}");
			nameSpace.Add($"{Config.CSharp.Entity.NameSpace}.{item.Schema}.Filter");
			nameSpace.Add($"{Config.CSharp.EntityFramework.NameSpace}");
			nameSpace.Add($"{Config.CSharp.EntityFramework.NameSpace}.{item.Schema}");

			if (Config.CSharp.Repository.Contract != null && Config.CSharp.Repository.Contract.Execute)
			{
				nameSpace.Add($"{Config.CSharp.Repository.Contract.NameSpace}.{item.Schema}");
				parentClass = $"RepositoryEFBase<{item.Name}Entity, {item.Name}Filter, {item.Name}, {Config.CSharp.EntityFramework.ContextName}>, I{item.Name}Business";
			}

			var appClass = Config.CSharp.ApplicationClasses.FirstOrDefault(c => c.Name == item.Name && c.Schema == item.Schema);

			if (Config.CSharp.UsingApplicationBase && appClass != null)
			{
				nameSpace = new List<string>
				{
					$"{Config.CSharp.Repository.Contract.NameSpace}.{item.Schema}"
				};

				parentClass = $"{appClass.NamespaceBusiness}.{item.Name}Business, I{item.Name}Business";
			}

			var builder = new StringBuilderIndented();

			builder
				.ClassInit($"{item.Name}Business", parentClass, $"{Config.CSharp.Repository.Concrecte.NameSpace}.{item.Schema}", ClassVisibility.Public, nameSpace.ToArray())
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
			var parentClass = $"IRepositoryEF<{item.Name}Entity, {item.Name}Filter>";
			var appClass = Config.CSharp.ApplicationClasses.FirstOrDefault(c => c.Name == item.Name && c.Schema == item.Schema);

			if (Config.CSharp.UsingApplicationBase && appClass != null)
			{
				parentClass = $"{appClass.NamespaceContract}.I{item.Name}Business";
				nameSpace = new List<string>();
			}

			builder
				.InterfaceInit($"I{item.Name}Business", parentClass, $"{Config.CSharp.Repository.Contract.NameSpace}.{item.Schema}", ClassVisibility.Public, nameSpace.ToArray())
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

			var parentClass = $"SpecificationQuery<{item.Name}, {item.Name}Filter>";
			var appClass = Config.CSharp.ApplicationClasses.FirstOrDefault(c => c.Name == item.Name && c.Schema == item.Schema);

			if (Config.CSharp.UsingApplicationBase && appClass != null)
			{
				parentClass = $"{appClass.NamespaceQuery}.{item.Name}Query";
				nameSpace = new List<string>();
				builder
					.ClassInit($"{item.Name}Query", parentClass, $"{Config.CSharp.Repository.Concrecte.NameSpace}.{item.Schema}.Query", ClassVisibility.Public, nameSpace.ToArray())
					.ClassEnd();
			}
			else
			{
				builder
					.ClassInit($"{item.Name}Query", parentClass, $"{Config.CSharp.Repository.Concrecte.NameSpace}.{item.Schema}.Query", ClassVisibility.Public, nameSpace.ToArray())
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
			}

			Writer.WriteFile(path, file, builder, false);
		}

		public void WriteValidation(DataBaseTable item)
		{
			var path = Path.Combine(Config.CSharp.Repository.Concrecte.Path, item.Schema, "Validate");
			var file = $"{item.Name}Validate.cs";


			var nameSpace = new List<string> { "System", "System.Threading.Tasks", "XCommon.Application.Executes", "XCommon.Patterns.Specification.Validation", "XCommon.Patterns.Specification.Validation.Extensions" };
			nameSpace.Add($"{Config.CSharp.Entity.NameSpace}.{item.Schema}");

			var builder = new StringBuilderIndented();

			var parentClass = $"SpecificationValidation<{item.Name}Entity>";
			var appClass = Config.CSharp.ApplicationClasses.FirstOrDefault(c => c.Name == item.Name && c.Schema == item.Schema);

			if (Config.CSharp.UsingApplicationBase && appClass != null)
			{
				nameSpace = new List<string>();
				parentClass = $"{appClass.NamespaceValidate}.{item.Name}Validate";

				builder
					.ClassInit($"{item.Name}Validate", parentClass, $"{Config.CSharp.Repository.Concrecte.NameSpace}.{item.Schema}.Validate", ClassVisibility.Public, nameSpace.ToArray())
					.InterfaceEnd();
			}
			else
			{
				builder
					.ClassInit($"{item.Name}Validate", parentClass, $"{Config.CSharp.Repository.Concrecte.NameSpace}.{item.Schema}.Validate", ClassVisibility.Public, nameSpace.ToArray())
					.AppendLine($"public override async Task<bool> IsSatisfiedByAsync({item.Name}Entity entity, Execute execute)")
					.AppendLine("{")
					.IncrementIndent()
					.AppendLine("var spefications = NewSpecificationList()")
					.IncrementIndent()
					.AppendLine(".AndIsValid(e => e.Key != Guid.Empty, \"Default key isn't valid\");")
					.DecrementIndent()
					.AppendLine()
					.AppendLine("return await CheckSpecificationsAsync(spefications, entity, execute);")
					.DecrementIndent()
					.AppendLine("}")
					.InterfaceEnd();
			}

			Writer.WriteFile(path, file, builder, false);
		}
	}
}
