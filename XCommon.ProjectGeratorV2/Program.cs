using System;
using System.Collections.Generic;
using XCommon.Application.CommandLine;
using XCommon.Application.ConsoleX;
using XCommon.ProjectGeratorV2.Application;

namespace XCommon.ProjectGeratorV2
{
    class Program
    {
        private static ApplicationBase App { get; set; }

        static void Main(string[] args)
        {
            if (SetUp(args))
            {
                using (Spinner sp = new Spinner(SpinnerSequence.Dots, true, 2))
                {
                    App.Run();
                }
            }
        }

        private static bool SetUp(string[] args)
        {
            CommandLineApplication result = new CommandLineApplication(false)
            {
                Name = "xpg",
                FullName = "XCommon Project Generator",
                Description = "Generate new C# project with support to Angular2 and Material Design",
            };

            var help = result.HelpOption("--help");
            var csharp = result.Option("--csharp", "Create new C# application", CommandOptionType.NoValue);
            var node = result.Option("--node", "Create new NODE application", CommandOptionType.NoValue);
            
            result.OnExecute(() =>
            {
                bool error = false;
                List<string> messages = new List<string>();

                if (!csharp.HasValue() && !node.HasValue())
                {
                    messages.Add("You need to chose which type of application you whant to create, use --node or --csharp");
                    error = true;
                }

                if (!csharp.HasValue() && !node.HasValue())
                {
                    messages.Add("You can't create both, choose --node or --csharp");
                    error = true;
                }

                if (!error && csharp.HasValue())
                {
                    App = new Application.CSharp.CSharpApplication(args);
                }

                if (!error && node.HasValue())
                {
                    App = new Application.Node.NodeApplication(args);
                }

                if (error)
                {
                    ApplicationBase.PrintLogo();

                    result.ShowHelp();
                    Console.WriteLine();
                    Console.WriteLine("  - Errors");
                    messages.ForEach(msg => Console.WriteLine($"    * {msg}"));
                    return -1;
                }

                return 0;
            });

            return result.Execute(args) == 0;
        }
    }
}
