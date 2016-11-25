using System;
using XCommon.Application.CommandLine;
using XCommon.CodeGerator.Angular;
using XCommon.CodeGerator.Core.DataBase;
using XCommon.CodeGerator.CSharp;
using XCommon.CodeGerator.TypeScript;

namespace XCommon.CodeGerator
{
	public class Generator
    {
		public Generator(Configuration config)
		{
			DBReader = new DataBaseRead();
			Config = config;

			if (Config.CSharp != null && Config.CSharp.DataBase != null)
			{
				Config.DataBaseItems = DBReader.ReadDataBase(Config.CSharp.DataBase);
				Config.CSharp.DataBaseItems = Config.DataBaseItems;
			}
			else
			{
				Console.WriteLine("Missing C# Database Config");
			}
		}

		private DataBaseRead DBReader { get; set; }

		private Configuration Config { get; set; }

		public int Run(string[] args)
		{
			CommandLineApplication app = new CommandLineApplication(false)
			{
				Name = "Prospect code generator",
				Description = "Runs different methods as dnx commands to help you to create some of picies of code",
			};

			var help = app.HelpOption("-?|--help");
			var angular = app.Option("-a|--angular", "Angular code generation", CommandOptionType.NoValue);
			var csharp = app.Option("-c|--csharp", "C# code generarion", CommandOptionType.NoValue);
			var typeScript = app.Option("-t|--typescript", "TypeScript code generarion", CommandOptionType.NoValue);

			app.OnExecute(() => {

				if (csharp.HasValue())
				{
					CSharpRunner runner = new CSharpRunner();
					return runner.Run(Config.CSharp);
				}

				if (angular.HasValue())
				{
					AngularRunner runner = new AngularRunner();
					return runner.Run(Config.Angular, args);
				}

				if (typeScript.HasValue())
				{
					TypeScriptRunner runner = new TypeScriptRunner();
					return runner.Run(Config.TypeScript);
				}

				app.ShowHelp();
				return 0;
			});

			return app.Execute(args);
		}
	}
}
