using XCommon.CodeGeneratorV2.Core;
using XCommon.CodeGeneratorV2.Core.DataBase;
using XCommon.Patterns.Ioc;

namespace XCommon.CodeGeneratorV2.CSharp
{
	internal class CSharpRunner : BaseRunner
	{
		#region Inject
		[Inject]
		private ICSharpRepositoryWritter CSharpRepositoryWritter { get; set; }

		[Inject]
		private ICSharpDataWritter CSharpDataWritter { get; set; }

		[Inject]
		private ICSharpEntityWritter CSharpEntityWritter { get; set; }

		[Inject]
		private ICSharpFactoryWritter CSharpFactoryWritter { get; set; }

		[Inject]
		public IDataBaseRead DataBaseRead { get; set; }
		#endregion

		internal int Run(GeneratorConfig config)
		{
			config.DataBaseItems = config.DataBaseItems ?? DataBaseRead.Read("");

			foreach (var schema in config.DataBaseItems)
			{
				foreach (var table in schema.Tables)
				{
					CSharpEntityWritter.WriteEntity(table);
					CSharpEntityWritter.WriteFilter(table);

					CSharpRepositoryWritter.WriteConcrete(table);
					CSharpRepositoryWritter.WriteContract(table);
					CSharpRepositoryWritter.WriteQuery(table);
					CSharpRepositoryWritter.WriteValidation(table);
				}
			}

			return 0;
		}
	}
}
