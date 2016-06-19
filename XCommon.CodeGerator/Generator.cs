using System.Collections.Generic;
using XCommon.Application.CommandLine;
using XCommon.CodeGerator.Angular2;
using XCommon.CodeGerator.Business;
using XCommon.CodeGerator.Configuration;
using XCommon.CodeGerator.DataBaseReader;
using XCommon.CodeGerator.Entity;

namespace XCommon.CodeGerator
{
	public static class Generator
    {
		private static List<ItemGroup> _ItemGroups;
		
		internal static Config Configuration { get; set; }

		internal static List<ItemGroup> ItemGroups
		{
			get
			{
				if (_ItemGroups == null)
				{
					var reader = new DataBaseRead();
					_ItemGroups = reader.ReadDataBase();
				}

				return _ItemGroups;
			}
		}

		public static void LoadConfig(Config config)
		{
			Configuration = config;
		}

		public static void LoadConfig(string file)
		{
			Configuration = new Config { };
		}

		public static int Run(string[] args)
		{
			CommandLineApplication app = new CommandLineApplication(false)
			{
				Name = "Prospect code generator",
				Description = "Runs different methods as dnx commands to help you to create some of picies of code",
			};

			var help = app.HelpOption("-?|--help");
			var angular = app.Option("-a|--angular", "Angular code generation", CommandOptionType.NoValue);
			var csharp = app.Option("-c#|--csharp", "C# code generarion", CommandOptionType.NoValue);
			var typeScript = app.Option("-ts|--typescript", "TypeScript code generarion", CommandOptionType.NoValue);

			app.OnExecute(() => {

				if (csharp.HasValue())
				{
					BusinessHelper businessHelper = new BusinessHelper();
					return businessHelper.RunAll();
				}

				if (angular.HasValue())
				{
					AngularHelper angularHelper = new AngularHelper();
					return angularHelper.Run(args);
				}

				app.ShowHelp();
				return 0;
			});

			return app.Execute(args);
		}
	}
}
