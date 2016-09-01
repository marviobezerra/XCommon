using System.Collections.Generic;
using XCommon.ProjectGerator.Application.CSharp.Extension;
using XCommon.Application.CommandLine;
using XCommon.ProjectGerator.Application.CSharp.Writter;
using XCommon.ProjectGerator.Application.CSharp.Enumeration;
using XCommon.ProjectGerator.Application.Commands;
using XCommon.ProjectGerator.Application.Angular;

namespace XCommon.ProjectGerator.Application.CSharp
{
    public class CSharpApplication : ApplicationBase
    {
        public CSharpApplication(string[] args) : base(args)
        {
        }

        protected override void SetUp(string[] args)
        {
            args = RemoveArg("--csharp", args);

            CommandLineApplication result = new CommandLineApplication(false)
            {
                Name = "xpg",
                FullName = "XCommon Project Generator",
                Description = "Generate new C# project with support to Angular2 and Material Design",
            };

            var help = result.HelpOption("--help");

            var simple = result.Option("-s|--simple", "Create new C# application, without business layer", CommandOptionType.NoValue);
            
            var path = result.Option("-p|--path", "Set the path to the new application", CommandOptionType.SingleValue);
            var name = result.Option("-n|--name", "Set the name of the new application", CommandOptionType.SingleValue);

            var noangular = result.Option("--noangular", "Create new C# application, without angular2 on web project", CommandOptionType.NoValue);
            var notest = result.Option("--notest", "Create new C# application, without test project", CommandOptionType.NoValue);
            var nogenerator = result.Option("--nogenerator", "Create new C# application, without code generator project", CommandOptionType.NoValue);

            result.OnExecute(() => 
            {
                bool error = false;
                List<string> messages = new List<string>();

                if (!path.HasValue())
                {
                    error = true;
                    messages.Add("Path not specified");                    
                }

                if (!name.HasValue())
                {
                    error = true;
                    messages.Add("Name not specified");
                }

                if (error)
                {
                    result.ShowHelp();
                    Console.WriteLine();
                    Console.WriteLine("  - Errors");
                    messages.ForEach(msg => Console.WriteLine($"    * {msg}"));
                    return -1;
                }

                var config = new CSharpApplicationConfig
                {
                    Name = name.Value(),
                    Path = path.Value(),
                    Full = !simple.HasValue(),
                    Angular = !noangular.HasValue(),
                    Generator = !nogenerator.HasValue(),
                    Test = !notest.HasValue()
                };

                Create(config);

                return 0;
            });

            PrintLogo();
            result.Execute(args);
        }

        private void Create(CSharpApplicationConfig config)
        {
            var solution = Commands
                .AddCSharpSolution(config.Path, config.Name);

            if (config.Full)
                CreateBusinessProjects(solution);

            var webProject = solution
                .AddAspNetProject("View.Web", config.Full);

            CommandsPost.Add(new CommandShell(new CommandShellParam { Name = "Restoring DotNet packages", Command = "dotnet", Arguments = "restore", Directory = solution.Parameter.Path }));

            if (config.Angular)
            {
                webProject
                    .AddAngularGulp(config.Full);

                AngularApplication angular = new AngularApplication(webProject.Parameter.SolutionParam.SolutionName, webProject.Parameter.Path, "wwwroot");

                Commands.AddRange(angular.Commands);
                CommandsPost.AddRange(angular.CommandsPost);
            }
        }

        private void CreateBusinessProjects(CreateSolution solution)
        {
            CreateProject businessConcrecte = solution
                .AddCSharpProject("Business.Concrecte")
                .AddCSharpProjectJson(ProjectJson.BusinessConcrecte);

            CreateProject businessContract = solution
                .AddCSharpProject("Business.Contract")
                .AddCSharpProjectJson(ProjectJson.BusinessContract);

            CreateProject businessEntity = solution
                .AddCSharpProject("Business.Entity")
                .AddCSharpProjectJson(ProjectJson.BusinessEntity);

            CreateProject businessData = solution
                .AddCSharpProject("Business.Data")
                .AddCSharpProjectJson(ProjectJson.BusinessData);

            CreateProject businessFactory = solution
                .AddCSharpProject("Business.Factory")
                .AddCSharpProjectJson(ProjectJson.BusinessFactory)
                .AddCSharpFactoryDo();

            CreateProject businessResource = solution
                .AddCSharpProject("Business.Resource")
                .AddCSharpProjectJson(ProjectJson.BusinessResource);

            CreateProject businessGenerator = solution
                .AddCSharpProject("CodeGenerator")
                .AddCSharpProjectJson(ProjectJson.BusinessCodeGenerator);

            var safeName = businessGenerator.Parameter.SolutionParam.SolutionName
                .Replace(".", string.Empty);

            var generatorTemplate = Resources.CSharp.GeneratorProgram
                .Replace("[{name}]", businessGenerator.Parameter.SolutionParam.SolutionName)
                .Replace("[{nameSafe}]", safeName);

            businessGenerator
                .AddFile(".", "Program.cs", generatorTemplate);
        }
    }
}
