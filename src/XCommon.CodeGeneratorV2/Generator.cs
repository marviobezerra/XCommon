using XCommon.Application.CommandLine;
using XCommon.CodeGeneratorV2.Angular;
using XCommon.CodeGeneratorV2.CSharp;
using XCommon.CodeGeneratorV2.TypeScript;

namespace XCommon.CodeGeneratorV2
{
	public class Generator
    {
		public Generator(GeneratorConfig config)
		{
			Factory.Do(config);
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

				//if (csharp.HasValue())
				//{
					var runner = new CSharpRunner();
					return runner.Run();
				//}

				//if (angular.HasValue())
				//{
				//	var runner = new AngularRunner();
				//	return runner.Run(args);
				//}

				//if (typeScript.HasValue())
				//{
				//	var runner = new TypeScriptRunner();
				//	return runner.Run();
				//}

				//app.ShowHelp();
				//return 0;
			});

			return app.Execute(args);
		}
	}
}
