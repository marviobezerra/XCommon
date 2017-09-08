using System;
using System.Threading.Tasks;
using XCommon.CodeGeneratorV2;
using XCommon.CodeGeneratorV2.CSharp.Configuration;

namespace XCommon.Test.CodeGeneratorV2
{
    class Program
    {
        static async Task Main(string[] args)
        {
			await Task.Factory.StartNew(() =>
			{
				var generator = new Generator(GetConfig());
				generator.Run(args);
			});
        }

		static GeneratorConfig GetConfig()
		{
			return new GeneratorConfig
			{
				DataBase = new DataBaseConfig
				{
					ConnectionString = "Data Source=(local);Initial Catalog=MyPetLife;Integrated Security=False;User Id=dev;Password=dev;MultipleActiveResultSets=True;Application Name=MyPetLife-Dev;Connection Timeout=1200"
				},
				CSharp = new CSharpConfig
				{
					Factory = new CSharpProjectConfig
					{
						Execute = false,
						Path = "D:\\Gen\\Business\\Factory",
						NameSpace = "XCommon.Factory"
					},
					Repository = new CSharpRepositoryConfig
					{
						Execute = true,
						Concrecte = new CSharpProjectConfig
						{
							Path = "D:\\Gen\\Business\\Concrecte",
							NameSpace = "XCommon.Concrecte"
						},
						Contract = new CSharpProjectConfig
						{
							Path = "D:\\Gen\\Business\\Contract",
							NameSpace = "XCommon.Contract"
						}
					},
					EntityFramework = new CSharpEntityFrameworkConfig
					{
						Execute = true,
						ContextName = "Sample",
						NameSpace = "XCommon.DataBase",
						Path = "D:\\Gen\\DataBase"						
					},
					Entity = new CSharpEntityConfig
					{
						Execute = true,
						ExecuteEntity = true,
						ExecuteFilter = true,
						Path = "D:\\Gen\\Entity",
						NameSpace = "XCommon.Entity"
					}
				}
			};
		}
	}
}
