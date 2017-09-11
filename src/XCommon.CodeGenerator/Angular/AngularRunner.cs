using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XCommon.Application.CommandLine;
using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.Angular;
using XCommon.CodeGenerator.Angular.Implementation;
using XCommon.Patterns.Ioc;

namespace XCommon.CodeGenerator.Angular
{
	public class AngularRunner : BaseRunner
	{
		[Inject]
		private IComponentWriter ComponentWriter { get; set; }

		[Inject]
		private IServiceWriter ServiceWriter { get; set; }

		public int Run(string[] args)
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

				var moduleName = module.HasValue() ? module.Value() : "";
				var featureName = feature.HasValue() ? feature.Value() : "";

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

					var path = ParsePath(ItemType.Component, moduleName, featureName);

					ComponentWriter.Run(path, moduleName, featureName, GetItems(appCommand, name));
					return 0;
				}

				if (service.HasValue())
				{
					if (string.IsNullOrEmpty(name.Value))
					{
						Console.WriteLine("Name of component/feature doesn't specified");
						return -1;
					}

					var path = ParsePath(ItemType.Service, moduleName, featureName);

					ServiceWriter.Run(path, GetItems(appCommand, name));
					return 0;
				}

				appCommand.ShowHelp();
				return 0;
			});

			var arguments = args.ToList();
			arguments.Remove("-a");

			return appCommand.Execute(arguments.ToArray());
		}

		private string ParsePath(ItemType type, string module, string feature)
		{
			var currentPath = Directory.GetCurrentDirectory();

			switch (type)
			{

				case ItemType.Directive:
					return Path.Combine(Config.Angular.Path, module, "directives");
				case ItemType.Service:
					return Path.Combine(Config.Angular.Path, module, "services");
				case ItemType.Pipe:
					return Path.Combine(Config.Angular.Path, module, "pipes");
				case ItemType.Component:
				default:
					return Path.Combine(Config.Angular.Path, module, "components", feature);
			}
		}

		private List<string> GetItems(CommandLineApplication AppCommand, CommandArgument name)
		{
			var items = new List<string>
			{
				name.Value
			};

			items.AddRange(AppCommand.RemainingArguments);
			return items;
		}
	}
}
