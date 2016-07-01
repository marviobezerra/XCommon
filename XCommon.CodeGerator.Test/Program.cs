using System;
using System.Collections.Generic;
using XCommon.CodeGerator.Configuration;

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
					StyleInclude = new List<string>(),
					StylePath = @"D:\A\Web\App\Style",
					StyleMain = @"D:\A\Web\App\Style\Main.scss"
				},
				DataBase = new ConfigDataBase
				{
					ConnectionString = "Data Source=(local);Initial Catalog=MyPetLife;Integrated Security=False;User Id=dev;Password=dev;MultipleActiveResultSets=True;Application Name=MyPetLife-Dev;Connection Timeout=1200",
					ContextName = "MyPetLifeContext",
					DataNameSpace = "Prospect.MyPetLife.Business.Data",
					DataPath = @"D:\A\Data",
				},
				Business = new ConfigBusiness
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
				Entity = new ConfigEntity
				{
					Path = @"D:\A\Web\App\Entity",
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
			};
		}
    }
}
