using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XCommon.Application.CommandLine;
using XCommon.CodeGenerator.Angular.Configuration;
using XCommon.CodeGenerator.Angular.Writter;

namespace XCommon.CodeGenerator.Angular
{
	public class AngularRunner
	{
		internal int Run(AngularConfig config, string[] args)
		{
			var appCommand = new CommandLineApplication(false)
			{
				Name = "Angular Generator",
				FullName = "Angular Generator",
				Description = "Angular Generator can generate Component, Service and Pipe",
			};

			var help = appCommand.HelpOption("-h|--help");
			var name = appCommand.Argument("[terms]", "Name of items to be generate");

			var module = appCommand.Option("-m|--module", "Module", CommandOptionType.SingleValue);
			var feature = appCommand.Option("-f|--feature", "Feature", CommandOptionType.SingleValue);
			var component = appCommand.Option("-c|--component", "Generate a new Component with HTML, TS and SCSS", CommandOptionType.NoValue);
			var service = appCommand.Option("-s|--service", "Generate a new Angular service", CommandOptionType.NoValue);

			appCommand.OnExecute(() =>
			{
				if (appCommand.OptionHelp.HasValue())
				{
					appCommand.ShowHelp();
					return 0;
				}

				IndexExport index = new IndexExport();
                string moduleName = module.HasValue() ? module.Value() : "";
                string featureName = feature.HasValue() ? feature.Value() : "";

				if (component.HasValue())
				{
					var erro = false;

					if (!feature.HasValue())
					{
						Console.WriteLine("Name of component doesn't specified");
						erro = true;
					}

					if (string.IsNullOrEmpty(name.Value) || !feature.HasValue())
					{
						Console.WriteLine("Name of component doesn't specified");
						erro = true;
					}

					if (erro)
					{
						return -1;
					}

					Component componentWritter = new Component();

					string path = ParsePath(config, ItemType.Component, moduleName, featureName);

					componentWritter.Run(path, moduleName, featureName, config.ComponentHtmlRoot, config.StyleInclude, GetItems(appCommand, name));
					index.Run(path);

					return 0;
				}

				if (service.HasValue())
				{
					if (string.IsNullOrEmpty(name.Value))
					{
						Console.WriteLine("Name of component/feature doesn't specified");
						return -1;
					}

					Service serviceWritter = new Service();
					string path = ParsePath(config, ItemType.Service, moduleName, featureName);

					serviceWritter.Run(path, GetItems(appCommand, name));
					index.Run(path);

					return 0;
				}

				appCommand.ShowHelp();
				return 0;
			});

			var arguments = args.ToList();
			arguments.Remove("-a");

			return appCommand.Execute(arguments.ToArray());
		}

		private string ParsePath(AngularConfig config, ItemType type, string module, string feature)
		{
			string currentPath = Directory.GetCurrentDirectory();

			switch (type)
			{

				case ItemType.Directive:
                    return Path.Combine(config.AppPath, module, "directives");
				case ItemType.Service:
                    return Path.Combine(config.AppPath, module, "services");
                case ItemType.Pipe:
                    return Path.Combine(config.AppPath, module, "pipes");
                case ItemType.Component:
				default:
                    return Path.Combine(config.AppPath, module, "components", feature);
            }			
		}

		private List<string> GetItems(CommandLineApplication AppCommand, CommandArgument name)
		{
			List<string> items = new List<string>();
			items.Add(name.Value);
			items.AddRange(AppCommand.RemainingArguments);
			return items;
		}
	}
}
