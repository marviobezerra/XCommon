using System;
using XCommon.Application.CommandLine;
using XCommon.CodeGeneratorV2.Angular;
using XCommon.CodeGeneratorV2.CSharp;
using XCommon.CodeGeneratorV2.CSharp.Implementation;
using XCommon.CodeGeneratorV2.TypeScript;
using XCommon.Patterns.Ioc;

namespace XCommon.CodeGeneratorV2
{
    public class Generator
    {
		private GeneratorConfig Config { get; set; }
		
		private void Map()
		{
			Kernel.Map<ICSharpRepositoryWritter>().To<CSharpRepositoryWritter>();
			Kernel.Map<ICSharpDataWritter>().To<CSharpDataWritter>();
			Kernel.Map<ICSharpEntityWritter>().To<CSharpEntityWritter>();
			Kernel.Map<ICSharpFactoryWritter>().To<CSharpFactoryWritter>();
			Kernel.Map<ICSharpUnitTestWritter>().To<CSharpUnitTestWritter>();
		}

		public int Run(string[] args)
		{
			var app = new CommandLineApplication(false)
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
					var runner = new CSharpRunner();
					return runner.Run(Config);
				}

				if (angular.HasValue())
				{
					var runner = new AngularRunner();
					return runner.Run(Config, args);
				}

				if (typeScript.HasValue())
				{
					var runner = new TypeScriptRunner();
					return runner.Run(Config);
				}

				app.ShowHelp();
				return 0;
			});

			return app.Execute(args);
		}
	}
}
