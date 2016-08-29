using System.Collections.Generic;
using XCommon.ProjectGeratorV2.Application.CSharp.Writter;

namespace XCommon.ProjectGeratorV2.Application.CSharp.Extension
{
    public static class CSharpWebExtension
    {
        private static string GetSafeName(string name)
        {
            return name
                .Replace(".", string.Empty);
        }

        public static CreateProject AddAspNetProject(this CreateSolution solution, string name, bool full)
        {
            CreateProject project = new CreateProject(new CreateProjectParam(solution.Parameter, name) { Template = Resources.AspNet.ProjectXproj });

            project
                .AddAspNetProjectJson(full)
                .AddAspNetWebConfig()
                .AddAspNetStart(full)
                .AddAspNetSettings();

            solution.Add(project);

            return project;
        }

        public static CreateProject AddAspNetStart(this CreateProject project, bool full)
        {
            string appStartUpTemplate = Resources.AspNet.Startup
                .Replace("[{namespace}]", $"{project.Parameter.ProjectName}");

            appStartUpTemplate = appStartUpTemplate
                    .Replace("[{factoryUsing}]", !full ? string.Empty : $"using {project.Parameter.SolutionParam.SolutionName}.Business.Factory;")
                    .Replace("[{factoryInit}]", !full ? string.Empty : "Register.Do(appSettings.UnitTest);")
                    .Replace("[{name}]", GetSafeName(project.Parameter.SolutionParam.SolutionName));

            string appProgramTemplate = Resources.AspNet.Program
                .Replace("[{namespace}]", $"{project.Parameter.ProjectName}");

            string appControllerTemplate = Resources.AspNet.HomeController
                .Replace("[{namespace}]", $"{project.Parameter.ProjectName}.Controllers");

            CreateFile appStartUp = new CreateFile(new CreateFileParam(project.Parameter, "Code", "Startup.cs") { Template = appStartUpTemplate });
            project.Add(appStartUp);

            CreateFile appProgram = new CreateFile(new CreateFileParam(project.Parameter, "Code", "Program.cs") { Template = appProgramTemplate });
            project.Add(appProgram);

            CreateFile appController = new CreateFile(new CreateFileParam(project.Parameter, "Controllers", "HomeController.cs") { Template = appControllerTemplate });
            project.Add(appController);

            return project;
        }

        public static CreateProject AddAspNetSettings(this CreateProject project)
        {
            var safeName = GetSafeName(project.Parameter.SolutionParam.SolutionName);

            var template = Resources.AspNet.AppSettings
                .Replace("[{name}]", safeName);

            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "appsettings.json") { Template = template });
            project.Add(file);
            return project;
        }

        public static CreateProject AddAspNetWebConfig(this CreateProject project)
        {
            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "web.config") { Template = Resources.AspNet.WebConfig });
            project.Add(file);
            return project;
        }

        public static CreateProject AddAspNetProjectJson(this CreateProject project, bool full)
        {
            var references = new List<string>();

            if (full)
            {
                references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Contract");
                references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Entity");
                references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Factory");
            }

            var template = CommandExtencionsCommon.MergeProjectJson(Resources.AspNet.ProjecJson, references);

            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "project.json") { Template = template });
            project.Add(file);
            return project;
        }

        public static CreateProject AddAngularGulp(this CreateProject project, bool full)
        {
            string template = Resources.Angular2.GulpFileSimple;

            if (full)
            {
                template = Resources.Angular2.GulpFile
                    .Replace("[{entity}]", $"{project.Parameter.SolutionParam.SolutionName}.Business.Entity")
                    .Replace("[{resource}]", $"{project.Parameter.SolutionParam.SolutionName}.Business.Resource")
                    .Replace("[{codegenerator}]", $"{project.Parameter.SolutionParam.SolutionName}.CodeGenerator");
            }

            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "gulpfile.js") { Template = template });
            project.Add(file);
            return project;
        }
    }
}
