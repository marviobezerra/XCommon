using System;
using System.Collections.Generic;
using System.Reflection;
using XCommon.CodeGeratorV2;
using XCommon.CodeGeratorV2.Configuration;

namespace XCommon.CodeGerator.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
			Generator.LoadConfig(GetConfig());
			Generator.Run(args);
        }

		private static Config GetConfig()
		{
			return new Config
			{
				Angular = new ConfigAngular {
					AppRoot = @"D:\A\Web\App",
					ComponentPath = @"D:\A\Web\App\Component",
					ServicePath = @"D:\A\Web\App\Service",
                    HtmlRoot = "/html/components",
					StyleInclude = new List<string>(),
				},
				DataBase = new ConfigDataBase
				{
					ConnectionString = "Data Source=(local);Initial Catalog=MyPetLife;Integrated Security=False;User Id=dev;Password=dev;MultipleActiveResultSets=True;Application Name=MyPetLife-Dev;Connection Timeout=1200",
					ContextName = "MyPetLifeContext",
					DataNameSpace = "Prospect.MyPetLife.Business.Data",
					DataPath = @"D:\A\Data",
				},
                CSharp = new ConfigCSharp
                {
					EntrityPath = @"D:\A\Entity",
					EntrityNameSpace = "Prospect.MyPetLife.Business.Entity",
					FactoryPath = @"D:\A\Factory",
					FacotryNameSpace = "Prospect.MyPetLife.Business.Facotry",
					ContractPath = @"D:\A\Contract",
					ContractNameSpace = "Prospect.MyPetLife.Business.Contract",
					ConcretePath = @"D:\A\Concret",
					ConcreteNameSpace = "Prospect.MyPetLife.Business.Concret",
				},
                TypeScript = new ConfigTypeScript
                {
					Entity = new ConfigEntity
                    {
                        Path = @"D:\A\Web\App\Entity",
                        Assemblys = new List<System.Reflection.Assembly>
                        {
                            typeof(Entity.Common.CitiesEntity).GetTypeInfo().Assembly
                        },
                        TypesExtra = new List<Type>
                        {
                            typeof(XCommon.Patterns.Repository.Executes.ExecuteMessageType),
                            typeof(XCommon.Patterns.Repository.Executes.ExecuteMessage),
                            typeof(XCommon.Patterns.Repository.Executes.Execute<>),
                            //typeof(Application.Login.LoginChangePasswordEntity),
						    //typeof(Application.Login.SignInEntity),
						    //typeof(Application.Login.SingUpEntity),
						    //typeof(Application.Login.LoginStatus)
					    }
                    },
                    Resource = new ConfigResource
                    {

                    }
                },
				
			};
		}
    }
}
