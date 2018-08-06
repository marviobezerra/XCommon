using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using XCommon.Application.CommandLine;
using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.CSharp;
using XCommon.CodeGenerator.TypeScript;
using XCommon.Patterns.Ioc;

namespace XCommon.CodeGenerator
{
	public class Generato2
	{
		private List<CustomRunner> Runners { get; set; }

		public Generato2(GeneratorConfig config, bool registerDefaults = true)
		{
			Kernel.Resolve(this);

			Runners = new List<CustomRunner>();

			if (registerDefaults)
			{
				RegisterDefaults();
			}
		}

		public int Run(string[] args)
		{
			var app = new CommandLineApplication(false)
			{
				Name = "XCommon code generator",
				Description = "Making our lifes easier. ",
			};

			PrintLogo(app);

			var help = app.HelpOption("-?|--help");

			Runners.ForEach(item =>
			{
				item.Command = app.Option($"-{item.TemplateShort}|--{item.TemplateLong}", item.Description, CommandOptionType.NoValue);
			});

			app.OnExecute(() =>
			{
				foreach (var item in Runners)
				{
					if (item.Command.HasValue())
					{
						var runner = (BaseRunner)Activator.CreateInstance(item.RunnerType);
						return runner.Run();
					}
				}

				app.ShowHelp();
				return 0;
			});

			return app.Execute(args);
		}

		public void RegisterRunner<TBaseRunner>(string templateShort, string templateLong, string description)
			where TBaseRunner : BaseRunner
		{
			var cleanerExpression = "[^a-zA-Z]";

			templateShort = $"{Regex.Replace(templateShort, cleanerExpression, "").ToLower()}";
			templateLong = $"{Regex.Replace(templateLong, cleanerExpression, "").ToLower()}";

			if (Runners.Any(c => c.TemplateShort == templateShort))
			{
				throw new Exception($"The short template {templateShort} is already mapped");
			}

			if (Runners.Any(c => c.TemplateLong == templateLong))
			{
				throw new Exception($"The long template {templateLong} is already mapped");
			}

			Runners.Add(new CustomRunner { TemplateShort = templateShort, TemplateLong = templateLong, Description = description, RunnerType = typeof(TBaseRunner) });
		}

		private void RegisterDefaults()
		{
			RegisterRunner<CSharpRunner>("c", "csharp", "C# code generarion");
			RegisterRunner<TypeScriptRunner>("t", "typescript", "TypeScript code generarion");
		}

		private void PrintLogo(CommandLineApplication app)
		{
			var logo = new List<string>
			{
				@"██╗  ██╗ ██████╗ ██████╗ ███╗   ███╗███╗   ███╗ ██████╗ ███╗   ██╗",
				@"╚██╗██╔╝██╔════╝██╔═══██╗████╗ ████║████╗ ████║██╔═══██╗████╗  ██║",
				@" ╚███╔╝ ██║     ██║   ██║██╔████╔██║██╔████╔██║██║   ██║██╔██╗ ██║",
				@" ██╔██╗ ██║     ██║   ██║██║╚██╔╝██║██║╚██╔╝██║██║   ██║██║╚██╗██║",
				@"██╔╝ ██╗╚██████╗╚██████╔╝██║ ╚═╝ ██║██║ ╚═╝ ██║╚██████╔╝██║ ╚████║",
				@"╚═╝  ╚═╝ ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚═╝     ╚═╝ ╚═════╝ ╚═╝  ╚═══╝"
			};

			Console.ForegroundColor = ConsoleColor.Blue;
			logo.ForEach(Console.WriteLine);

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(app.Name);

			Console.ResetColor();
		}
	}
}
