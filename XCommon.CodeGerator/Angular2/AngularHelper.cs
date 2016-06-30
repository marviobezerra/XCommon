using System;
using System.Collections.Generic;
using System.Linq;
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

                if (sass.HasValue())
                {
                    GeneratorComponent.UpdateSassReference();
                    return 0;
                }

                if (component.HasValue())
                {
                    var erro = false;

                    if (!feature.HasValue())
                    {
                        Console.WriteLine("Name of component doesn't specified");
                        erro = true;
                    }

                    if (name.Value.IsEmpty() || !feature.HasValue())
                    {
                        Console.WriteLine("Name of component doesn't specified");
                        erro = true;
                    }

                    if (erro)
                    {
                        return -1;
                    }


                    GeneratorComponent.Run(feature.Value(), GetItems(AppCommand, name));
                    return 0;
                }

                if (service.HasValue())
                {
                    if (name.Value.IsEmpty())
                    {
                        Console.WriteLine("Name of component/feature doesn't specified");
                        return -1;
                    }

                    GeneratorService.Run(GetItems(AppCommand, name));
                    return 0;
                }

                AppCommand.ShowHelp();
                return 0;
            });

            var arguments = args.ToList();
            arguments.Remove("-a");

            return AppCommand.Execute(arguments.ToArray());
        }

        private static List<string> GetItems(CommandLineApplication AppCommand, CommandArgument name)
        {
            List<string> items = new List<string>();
            items.Add(name.Value);
            items.AddRange(AppCommand.RemainingArguments);
            return items;
        }
    }
}
