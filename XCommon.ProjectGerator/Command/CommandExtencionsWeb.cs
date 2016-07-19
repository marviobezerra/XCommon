using System.Collections.Generic;

namespace XCommon.ProjectGerator.Command
{
    public static class CommandExtencionsWeb
    {
        public static CreateProject AddAspNetProject(this CreateSolution solution, string name)
        {
            CreateProject project = new CreateProject(new CreateProjectParam(solution.Parameter, name) { Template = Resources.AspNet.ProjectXproj });
            solution.Add(project);
            return project;
        }

        public static CreateProject AddAspNetStart(this CreateProject project, bool simple)
        {
            string appStartUpTemplate = Resources.AspNet.Startup
                .Replace("[{namespace}]", $"{project.Parameter.ProjectName}");

            appStartUpTemplate = appStartUpTemplate
                    .Replace("[{factoryUsing}]", simple ? string.Empty : $"using {project.Parameter.SolutionParam.SolutionName}.Business.Factory;")
                    .Replace("[{factoryInit}]", simple ? string.Empty : "Register.Do(appSettings.UnitTest);");

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
            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "appsettings.json") { Template = Resources.AspNet.AppSettings });
            project.Add(file);
            return project;
        }

        public static CreateProject AddAspNetWebConfig(this CreateProject project)
        {
            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "web.config") { Template = Resources.AspNet.WebConfig });
            project.Add(file);
            return project;
        }

        public static CreateProject AddAspNetProjectJson(this CreateProject project, bool simple)
        {
            var references = new List<string>();

            if (!simple)
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
    }
}
