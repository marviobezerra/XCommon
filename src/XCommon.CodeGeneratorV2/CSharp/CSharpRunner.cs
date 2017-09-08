using XCommon.CodeGeneratorV2.Core;
using XCommon.CodeGeneratorV2.Core.DataBase;
using XCommon.Patterns.Ioc;

namespace XCommon.CodeGeneratorV2.CSharp
{
	public class CSharpRunner : BaseRunner
	{
		#region Inject
		[Inject]
		private ICSharpRepositoryWriter CSharpRepositoryWriter { get; set; }

		[Inject]
		private ICSharpEnityFrameworkWriter CSharpEnityFrameworkWriter { get; set; }

		[Inject]
		private ICSharpEntityWriter CSharpEntityWriter { get; set; }

		[Inject]
		private ICSharpFactoryWriter CSharpFactoryWriter { get; set; }

		[Inject]
		private IDataBaseRead DataBaseRead { get; set; }
		#endregion

		internal int Run()
		{
			Config.DataBaseItems = Config.DataBaseItems ?? DataBaseRead.Read();

			if (Config.CSharp.Factory != null && Config.CSharp.Factory.Execute)
			{
				CSharpFactoryWriter.WriteFactory();
				CSharpFactoryWriter.WriteFactoryCustom();
			}

			if (Config.CSharp.EntityFramework != null && Config.CSharp.EntityFramework.Execute)
			{
				CSharpEnityFrameworkWriter.WriteContext();
			}

			foreach (var schema in Config.DataBaseItems)
			{
				foreach (var table in schema.Tables)
				{
					if (Config.CSharp.EntityFramework != null && Config.CSharp.EntityFramework.Execute)
					{
						CSharpEnityFrameworkWriter.WriteEntity(table);
						CSharpEnityFrameworkWriter.WriteEntityMap(table);
					}

					if (Config.CSharp.Entity != null)
					{
						if (Config.CSharp.Entity.Execute && Config.CSharp.Entity.ExecuteEntity)
						{
							CSharpEntityWriter.WriteEntity(table);
						}

						if (Config.CSharp.Entity.Execute && Config.CSharp.Entity.ExecuteFilter)
						{
							CSharpEntityWriter.WriteFilter(table);
						}
					}

					if (Config.CSharp == null || Config.CSharp.Repository == null || !Config.CSharp.Repository.Execute)
					{
						return 0;
					}

					if (Config.CSharp.Repository.Contract != null && Config.CSharp.Repository.Contract.Execute)
					{
						CSharpRepositoryWriter.WriteContract(table);
					}

					if (Config.CSharp.Repository.Concrecte != null && Config.CSharp.Repository.Concrecte.Execute)
					{
						CSharpRepositoryWriter.WriteConcrete(table);
						CSharpRepositoryWriter.WriteQuery(table);
						CSharpRepositoryWriter.WriteValidation(table);
					}
				}
			}

			return 0;
		}
	}
}
