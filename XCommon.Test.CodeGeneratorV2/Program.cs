using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using XCommon.CodeGenerator;
using XCommon.CodeGenerator.Angular.Configuration;
using XCommon.CodeGenerator.CSharp.Configuration;
using XCommon.CodeGenerator.TypeScript.Configuration;

namespace XCommon.Test.CodeGeneratorV2
{
	class Program
    {
        static async Task Main(string[] args)
        {
			await Task.Factory.StartNew(() =>
			{
				var generator = new Generator(GetConfig());
				//var x = new List<string> { "-t" };
				//generator.Run(x.ToArray());

				generator.Run(args);
			});
        }

		static GeneratorConfig GetConfig()
		{
			return new GeneratorConfig
			{
				Angular = new AngularConfig
				{
					Path = "D:\\Gen\\Angular",
					QuoteType = QuoteType.Double
				},
				TypeScript = new TypeScriptConfig
				{
					Entity = new TypeScriptEntityConfig
					{
						Path = "D:\\Gen\\Angular\\Entity",
						Assemblys = new List<Assembly> { typeof(GeneratorConfig).Assembly }
					}
				},
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
