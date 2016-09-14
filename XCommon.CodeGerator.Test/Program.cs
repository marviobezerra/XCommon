using System;
using System.Collections.Generic;
using System.Resources;
using XCommon.Application;
using XCommon.CodeGerator.Angular.Configuration;
using XCommon.CodeGerator.CSharp.Configuration;
using XCommon.CodeGerator.TypeScript.Configuration;

namespace XCommon.CodeGerator.Test
{
	public class Program
    {
        public static void Main(string[] args)
        {
			Generator generator = new Generator(GetConfig());
			generator.Run(args);
        }

        private static Configuration GetConfig()
        {
            return new Configuration
			{
				Angular = new AngularConfig
				{
					ComponentPath = @"D:\A\App\Component",
					DirectivePath = @"D:\A\App\Directive",
					ServicePath = @"D:\A\App\Service",
					PipePath = @"D:\A\App\Pipe",
					HtmlRoot = "html"
				},
				CSharp = new CSharpConfig
				{
					DataBase = new DataBaseConfig
					{
						ConnectionString = "Data Source=(local);Initial Catalog=MyPetLife;Integrated Security=False;User Id=dev;Password=dev;MultipleActiveResultSets=True;Application Name=MyPetLife-Dev;Connection Timeout=1200",
						ContextName = "MyPetLifeContext",
						NameSpace = "Prospect.MyPetLife.Business.Data",
						Path = @"D:\A\Data"
					}
				},
				TypeScript = new TypeScriptConfig { }				
            };
        }

		private static List<ApplicationCulture> GetCultures()
		{
			List<ApplicationCulture> result = new List<ApplicationCulture>();

			result.Add(new ApplicationCulture { Name = "pt-BR", Description = "Portugues" });
			result.Add(new ApplicationCulture { Name = "en-US", Description = "English" });

			return result;
		}

		private static Dictionary<Type, ResourceManager> GetResources()
		{
			Dictionary<Type, ResourceManager> result = new Dictionary<Type, ResourceManager>();

			result.Add(typeof(Resource.Form), Resource.Form.ResourceManager);
			result.Add(typeof(Resource.Messages), Resource.Messages.ResourceManager);

			return result;
		}
	}
}
