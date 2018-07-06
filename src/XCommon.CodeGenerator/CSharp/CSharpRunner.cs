using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.Core.DataBase;
using XCommon.Patterns.Ioc;

namespace XCommon.CodeGenerator.CSharp
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
		private ICSharpUnitTestWriter CSharpUnitTestWriter { get; set; }

		[Inject]
		private IDataBaseRead DataBaseRead { get; set; }
		#endregion

		internal int Run()
		{
			Config.DataBaseItems = Config.DataBaseItems ?? DataBaseRead.Read();

			if (Config.CSharp != null)
			{

				if (Config.CSharp.Factory != null && Config.CSharp.Factory.Execute)
				{
					CSharpFactoryWriter.WriteFactory();
					CSharpFactoryWriter.WriteFactoryCustom();
				}

				if (Config.CSharp.EntityFramework != null && Config.CSharp.EntityFramework.Execute)
				{
					CSharpEnityFrameworkWriter.WriteContext();
				}
			}

			foreach (var schema in Config.DataBaseItems)
			{
				foreach (var table in schema.Tables)
				{
					if (Config.CSharp != null)
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

						if (Config.CSharp.Repository != null && Config.CSharp.Repository.Execute)
						{
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

						if (Config.CSharp.UnitTest != null && Config.CSharp.UnitTest.Execute)
						{
							CSharpUnitTestWriter.WriteTests(table);
						}
					}
				}
			}

			return 0;
		}
	}
}
