using System;
using System.Collections.Generic;
using XCommon.Application.CommandLine;
using XCommon.ProjectGerator.Command;

namespace XCommon.ProjectGerator
{
    class Program
    {
        private static CommandsList Commands { get; set; } = new CommandsList();

        private static CommandsParams Params { get; set; } = new CommandsParams();

        static void Main(string[] args)
        {
            if (SetUp(args))
            {
                Commands = Params.Full
                    ? Commands.WebFull(Params.Path, Params.Name)
                    : Commands.WebSimple(Params.Path, Params.Name);
                
                Commands.Run();
            }
        }

        private static bool SetUp(string[] args)
        {
            CommandLineApplication result = new CommandLineApplication(false)
            {
                Name = "XCommon Project Generator",
                FullName = "XCommon Project Generator",
                Description = "Generate new C# project with support to Angular2 and Material Designe",
            };

            var full = result.Option("-f|--full", "Create the new application with N layers", CommandOptionType.NoValue);
            var simple = result.Option("-s|--simple", "Create the new application with only the web layers", CommandOptionType.NoValue);
            var path = result.Option("-p|--path", "Set the path to the new application", CommandOptionType.SingleValue);
            var name = result.Option("-n|--name", "Set the name of the new application", CommandOptionType.SingleValue);

            result.OnExecute(() =>
            {
                bool error = false;
                List<string> messages = new List<string>();

                if ((full.HasValue() && simple.HasValue()) || (!full.HasValue() && !simple.HasValue()))
                {
                    messages.Add("Choose FULL (-f) or SIMPLE (-s) to generate your new project");
                    error = true;
                }

                if (!name.HasValue())
                {
                    messages.Add("Choose the NAME (-n 'MyProject') to generate your new project");
                    error = true;
                }

                if (error)
                {
                    result.ShowHelp();
                    Console.WriteLine();
                    Console.WriteLine("  - Errors");
                    messages.ForEach(msg => Console.WriteLine($"    * {msg}"));
                    return -1;
                }

                Params.Full = full.HasValue();
                Params.Name = name.Value();
                Params.Path = path.HasValue() ? path.Value() : Environment.CurrentDirectory;

                return 0;
            });

            return result.Execute(args) == 0;
        }
    }
}
