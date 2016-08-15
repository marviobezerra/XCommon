using System.Collections.Generic;
using XCommon.Application.CommandLine;
using XCommon.CodeGeratorV2.Configuration;
using XCommon.CodeGeratorV2.Writer.Base;
using XCommon.CodeGeratorV2.Writer.Base.Entity;

namespace XCommon.CodeGeratorV2
{
    public class Generator
    {
        private static List<ItemGroup> _ItemGroups;

        internal static Config Configuration { get; set; }

        internal static List<ItemGroup> ItemGroups
        {
            get
            {
                if (_ItemGroups == null)
                {
                    var reader = new DataBaseRead();
                    _ItemGroups = reader.ReadDataBase();
                }

                return _ItemGroups;
            }
        }

        public static void LoadConfig(Config config)
        {
            Configuration = config;
        }

        public static void LoadConfig(string file)
        {
            Configuration = new Config { };
        }

        public static int Run(string[] args)
        {
            CommandLineApplication app = new CommandLineApplication(false)
            {
                Name = "XCommon code generator",
                Description = "Runs different methods as dnx commands to help you to create some of pieces of code",
            };

            var help = app.HelpOption("-?|--help");
            var angular = app.Option("-a|--angular", "Angular code generation", CommandOptionType.NoValue);
            var csharp = app.Option("-c|--csharp", "C# code generarion", CommandOptionType.NoValue);
            var typeScript = app.Option("-t|--typescript", "TypeScript code generarion", CommandOptionType.NoValue);
            var node = app.Option("-n|--node", "Node code generarion", CommandOptionType.NoValue);

            app.OnExecute(() => {

                if (csharp.HasValue())
                {
                    var csharpWriter = new Writer.CSharp.Generator();
                    return csharpWriter.Run(args);
                }

                if (angular.HasValue())
                {
                    var angularWriter = new Writer.Angular.Generator();
                    return angularWriter.Run(args);
                }

                if (typeScript.HasValue())
                {
                    var typeScriptWriter = new Writer.TypeScript.Generator();
                    return typeScriptWriter.Run(args);
                }

                if (node.HasValue())
                {
                    var nodetWriter = new Writer.Node.Generator();
                    return nodetWriter.Run(args);
                }

                app.ShowHelp();
                return 0;
            });

            return app.Execute(args);
        }
    }
}
