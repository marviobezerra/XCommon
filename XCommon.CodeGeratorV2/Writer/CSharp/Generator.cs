using XCommon.Application.CommandLine;

namespace XCommon.CodeGeratorV2.Writer.CSharp
{
    public class Generator
    {
        public int Run(string[] args)
        {
            CommandLineApplication app = new CommandLineApplication(false)
            {
                Name = "XCommon code generator - C#",
                Description = "Generate C# code",
            };

            var help = app.HelpOption("-?|--help");

            var newproject = app.Option("-n|--new", "Create new project", CommandOptionType.NoValue);
            var update = app.Option("-u|--update", "Update class generation", CommandOptionType.NoValue);
            var configCreate = app.Option("--newconfig", "Create JSON configuration", CommandOptionType.NoValue);
            var configLoad = app.Option("--config", "Create JSON configuration", CommandOptionType.NoValue);

            var path = app.Option("-p|--path", "Path of the new application", CommandOptionType.SingleValue);
            var name = app.Option("-n|--name", "Name of the new application", CommandOptionType.SingleValue);

            var projectSimple = app.Option("--simple", "Create just a web project", CommandOptionType.NoValue);
            var projectFull = app.Option("--full", "Create web project and class libraries", CommandOptionType.NoValue);

            var skipTest = app.Option("--notest", "Skip the creation of project test", CommandOptionType.NoValue);
            var skipAngular = app.Option("--noangular", "Skip the creation of angular dependency", CommandOptionType.NoValue);
            var skipAngularMaterial = app.Option("--nomaterial", "Skip the creation of angular-material dependency", CommandOptionType.NoValue);
            
            app.OnExecute(() => {

                if (configCreate.HasValue())
                {
                    return 0;
                }



                return 0;
            });

            return app.Execute(args);
        }
    }
}
