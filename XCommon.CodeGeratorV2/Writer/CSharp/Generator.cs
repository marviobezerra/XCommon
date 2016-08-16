using System.Linq;
using XCommon.Application.CommandLine;
using XCommon.Application.ConsoleX;
using XCommon.CodeGeratorV2.Writer.Base;

namespace XCommon.CodeGeratorV2.Writer.CSharp
{
    public class Generator : WriterBase
    {
        public int Run(string[] args)
        {
            CommandLineApplication app = new CommandLineApplication(false)
            {
                Name = "XCommon code generator - C#",
                Description = "Generate C# code",
            };

            var help = app.HelpOption("-?|--help");

            var newproject = app.Option("--newproject", "Create new project", CommandOptionType.NoValue);
            var update = app.Option("--updateproject", "Update project", CommandOptionType.NoValue);

            var path = app.Option("--path", "Project path", CommandOptionType.SingleValue);
            var name = app.Option("--name", "Project name", CommandOptionType.SingleValue);

            var projectSimple = app.Option("--simple", "Create just web project", CommandOptionType.NoValue);
            var projectFull = app.Option("--full", "Create web project and class libraries", CommandOptionType.NoValue);

            var skipTest = app.Option("--notest", "Skip the creation of project test", CommandOptionType.NoValue);
            var skipAngular = app.Option("--noangular", "Skip the creation of angular dependency", CommandOptionType.NoValue);
            var skipAngularMaterial = app.Option("--nomaterial", "Skip the creation of angular-material dependency", CommandOptionType.NoValue);

            var configCreate = app.Option("--newconfig", "Create JSON configuration", CommandOptionType.NoValue);
            var configLoad = app.Option("--config", "Specify JSON configuration", CommandOptionType.NoValue);

            app.OnExecute(() =>
            {
                if (configCreate.HasValue())
                {
                    return 0;
                }

                if (newproject.HasValue())
                {
                    if (!path.HasValue())
                        Error.Add("Project path not specified");

                    if (!name.HasValue())
                        Error.Add("Project name not specified");

                    if (Error.Any())
                        return 0;
                }

                if (!Error.Any())
                    app.ShowHelp();

                return 0;
            });

            var result = app.Execute(args);

            Warnings.ForEach(msg => Console.WriteLineYellow($"  #WARNING: {msg}"));

            if (Error.Count > 0)
            {
                Error.ForEach(msg => Console.WriteLineRed($"  #WARNING: {msg}"));
            }

            return result;
        }
    }
}
