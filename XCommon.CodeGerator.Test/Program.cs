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
					ConcretPath = @"D:\A\Concret",
					ConcretNameSpace = "Prospect.MyPetLife.Business.Concret",
				}
			};
		}
    }
}
