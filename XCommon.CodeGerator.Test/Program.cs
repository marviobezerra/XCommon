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
				DataBase = new ConfigDataBase
				{
					ConnectionString = "Data Source=(local);Initial Catalog=MyPetLife;Integrated Security=False;User Id=dev;Password=dev;MultipleActiveResultSets=True;Application Name=MyPetLife-Dev;Connection Timeout=1200"
				},
				Business = new ConfigBusiness
				{
					EntrityPath = @"D:\A\Entity",
					EntrityNameSpace = "Prospect.MyPetLife.Business.Entity"
				}
			};
		}
    }
}
