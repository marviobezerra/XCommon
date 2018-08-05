using System;
using System.Collections.Generic;
using XCommon.Application.CommandLine;
using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.CSharp;
using XCommon.CodeGenerator.TypeScript;

namespace XCommon.CodeGenerator
{
	public class Generator
	{
		private BaseRunner Runner { get; set; }

		private List<CustomRunner> CustomRunners { get; set; }

		public Generator(GeneratorConfig config)
		{
			Factory.Do(config);
			CustomRunners = new List<CustomRunner>();
		}

		public void RegisterRunner<TBaseRunner>(string templateShort, string templateLong, string description) where TBaseRunner : BaseRunner
		{
			var reservedShort = new List<string> { "-c", "-t", "-?" };
			var reservedLong = new List<string> { "--csharp", "--typescript", "--help" };

			if (reservedShort.Contains(templateShort))
			{
				throw new System.Exception($"The short template {templateShort} is reserved");
			}

			if (reservedLong.Contains(templateLong))
			{
				throw new System.Exception($"The short template {templateShort} is reserved");
			}

			CustomRunners.Add(new CustomRunner { TemplateShort = templateShort, TemplateLong = templateLong, Description = description, RunnerType = typeof(TBaseRunner) });
		}

		public int Run(string[] args)
		{
			var app = new CommandLineApplication(false)
			{
				Name = "Prospect code generator",
				Description = "Runs different methods as dnx commands to help you to create some of picies of code",
			};

			var help = app.HelpOption("-?|--help");
			var csharp = app.Option("-c|--csharp", "C# code generarion", CommandOptionType.NoValue);
			var typeScript = app.Option("-t|--typescript", "TypeScript code generarion", CommandOptionType.NoValue);

			CustomRunners.ForEach(item =>
			{
				item.Command = app.Option($"-{item.TemplateShort}|--{item.TemplateLong}", item.Description, CommandOptionType.NoValue);
			});

			app.OnExecute(() =>
			{

				if (csharp.HasValue())
				{
					Runner = new CSharpRunner();
					return Runner.Run();
				}

				if (typeScript.HasValue())
				{
					Runner = new TypeScriptRunner();
					return Runner.Run();
				}

				foreach (var item in CustomRunners)
				{
					if (item.Command.HasValue())
					{
						Runner = (BaseRunner)Activator.CreateInstance(item.RunnerType);
						return Runner.Run();
					}
				}

				app.ShowHelp();
				return 0;
			});

			return app.Execute(args);
		}
	}
}
