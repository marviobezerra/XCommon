using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using XCommon.Application.CommandLine;
using XCommon.Extensions.String;

namespace XCommon.CodeGerator.Angular2
{
    internal class AngularHelper
    {
		internal AngularHelper()
		{
			GeneratorComponent = new Component();
			GeneratorService = new Service();
		}

		private Service GeneratorService { get; set; }

		private Component GeneratorComponent { get; set; }

		internal int Run(string[] args)
		{
			var AppCommand = new CommandLineApplication(false)
			{
				Name = "Angular Generator",
				FullName = "Angular Generator",
				Description = "Angular Generator can generate Component, Service and Pipe",				
			};

			var help = AppCommand.HelpOption("-h|--help");
			var name = AppCommand.Argument("[terms]", "Name of items to be generate");

			var feature = AppCommand.Option("-f|--feature", "Feature", CommandOptionType.SingleValue);
			var component = AppCommand.Option("-c|--component", "Generate a new Component with HTML, TS and SCSS", CommandOptionType.NoValue);
			var service = AppCommand.Option("-s|--service", "Generate a new Angular service", CommandOptionType.NoValue);
			var sass = AppCommand.Option("-u|--sass", "Update SASS references", CommandOptionType.NoValue);


			AppCommand.OnExecute(() =>
			{
				if (AppCommand.OptionHelp.HasValue())
				{
					AppCommand.ShowHelp();
					return 0;
				}
				
				if (name.Value.IsEmpty())
				{
					Console.WriteLine("Name of component doesn't specified");
					return -1;
				}

				if (!feature.HasValue() && component.HasValue())
				{
					Console.WriteLine("Feature doesn't specified, use -f");
					return -1;
				}

				List<string> items = new List<string>();
				items.Add(name.Value);
				items.AddRange(AppCommand.RemainingArguments);
				
				if (component.HasValue())
				{
					GeneratorComponent.Run(feature.Value(), items);
					return 0;
				}

				if (service.HasValue())
				{
					GeneratorService.Run(items);
					return 0;
				}


				if (sass.HasValue())
				{
					//AngularGeneratorCommonTask.UpdateSassReference(GeneratorConfig.PathTheme, GeneratorConfig.PathPage);
					return 0;
				}

				AppCommand.ShowHelp();
				return 0;
			});

			var arguments = args.ToList();
			arguments.Remove("-a");
			
			return AppCommand.Execute(arguments.ToArray());
		}
    }
}
