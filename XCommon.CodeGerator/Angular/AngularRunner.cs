using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XCommon.Application.CommandLine;
using XCommon.CodeGerator.Angular.Configuration;
using XCommon.CodeGerator.Angular.Writter;

namespace XCommon.CodeGerator.Angular
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

					string path = ParsePath(config, feature.Value(), ItemType.Component);

					componentWritter.Run(path, feature.Value(), config.HtmlRoot, config.StyleInclude, GetItems(appCommand, name));
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
					string path = ParsePath(config, string.Empty, ItemType.Service);

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

		private string ParsePath(AngularConfig config, string extra, ItemType type)
		{
			string currentPath = Directory.GetCurrentDirectory();

			switch (type)
			{

				case ItemType.Directive:
					return string.IsNullOrEmpty(config.DirectivePath)
						? Path.Combine(currentPath, "app", "directive")
						: config.ComponentPath;
				case ItemType.Service:
					return string.IsNullOrEmpty(config.ServicePath)
						? Path.Combine(currentPath, "app", "service")
						: config.ComponentPath;
				case ItemType.Pipe:
					return string.IsNullOrEmpty(config.PipePath)
						? Path.Combine(currentPath, "app", "pipe")
						: config.ComponentPath;
				case ItemType.Component:
				default:
					return string.IsNullOrEmpty(config.ComponentPath)
						? Path.Combine(currentPath, "app", "component", extra)
						: config.ComponentPath;
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
