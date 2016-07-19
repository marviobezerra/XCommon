using XCommon.ProjectGerator.Command;
using XCommon.ProjectGerator.Enumeration;
using XCommon.ProjectGerator.Steps;
using System.Collections.Generic;
using System;
using XCommon.ProjectGerator.Properties;

namespace XCommon.ProjectGerator
{
    public class CommandsList
    {
        public CommandsList()
        {
            Commands = new List<ICommand>();
        }

        public List<ICommand> Commands { get; set; }

        public void Run()
        {
            Commands.ForEach(c => c.Run());
        }
    }

    public static class TypesCommands
    {
        public static CreateSolution AddSolution(this CommandsList command, string path, string name)
        {
            CreateSolution solution = new CreateSolution(new CreateSolutionParam { Path = path, SolutionName = name });
            command.Commands.Add(solution);

            return solution;
        }

        public static CreateProject AddProjectWeb(this CreateSolution solution, string name)
        {
            CreateProject project = new CreateProject(new CreateProjectParam(solution.Parameter, name) { Template = Properties.Resources.ProjectWebXproj });
            solution.Add(project);
            return project;
        }

        public static CreateProject AddProjectClass(this CreateSolution solution, string name)
        {
            CreateProject project = new CreateProject(new CreateProjectParam(solution.Parameter, name) { Template = Properties.Resources.ProjectClassXproj });
            solution.Add(project);
            return project;
        }

        public static CreateProject AddFile(this CreateProject project, string path, string name, string template)
        {
            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, path, name) { Template = template });
            project.Add(file);
            return project;
        }

        public static CreateProject AddProjectJson(this CreateProject project, ProjectJson type)
        {
            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "project.json") { Template = GetProjectJson(project, type) });
            project.Add(file);
            return project;
        }

        public static CreateProject AddFactoryDo(this CreateProject project)
        {
            string template = Resources.FactoryRegister
                .Replace("[{namespace}]", project.Parameter.ProjectName);

            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "Register.cs") { Template = template });
            project.Add(file);
            return project;
        }

        private static string GetProjectJson(CreateProject project, ProjectJson type)
        {
            var result = string.Empty;
            List<string> references = new List<string>();
            string include = string.Empty;

            switch (type)
            {
                case ProjectJson.BusinessCodeGenerator:
                    result = Resources.ProjectBusinessCodeGeneratorJson;
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Contract");
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Entity");
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Resource");
                    break;
                case ProjectJson.BusinessConcrecte:
                    result = Resources.ProjectBusinessConcrectJson;
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Contract");
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Data");
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Entity");
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Resource");
                    break;
                case ProjectJson.BusinessContract:
                    result = Resources.ProjectBusinessContractJson;
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Entity");
                    break;
                case ProjectJson.BusinessData:
                    result = Resources.ProjectBusinessDataJson;
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Entity");
                    break;
                case ProjectJson.BusinessEntity:
                    result = Resources.ProjectBusinessEntityJson;
                    break;
                case ProjectJson.BusinessFactory:
                    result = Resources.ProjectBusinessFactoryJson;
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Concrete");
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Contract");
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Data");
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Entity");
                    break;
                case ProjectJson.BusinessResource:
                    result = Resources.ProjectBusinessResourceJson;
                    break;
                case ProjectJson.ViewWeb:
                    result = Resources.ProjectViewWebJson;
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Contract");
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Entity");
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Factory");
                    break;
                case ProjectJson.ViewWebSimple:
                    result = Resources.ProjectViewWebJson;
                    break;
                default:
                    break;
            }

            var count = 1;
            references.ForEach(item => {
                string newLine = references.Count != count++ ? Environment.NewLine : string.Empty;
                include += $"    \"{item}\": \"1.0.0-*\",{newLine}";
            });
            result = result.Replace("[{include}]", include);

            return result;
        }
    }

    public static class TypesWeb
    {
        public static CreateProject AddAppStart(this CreateProject project, bool simple)
        {
            string appStartUpTemplate = Resources.WebAppStartStartup
                .Replace("[{namespace}]", $"{project.Parameter.ProjectName}");

            if (!simple)
            {
                appStartUpTemplate = appStartUpTemplate
                    .Replace("[{factoryUsing}]", $"using {project.Parameter.SolutionParam.SolutionName}.Business.Factory;")
                    .Replace("[{factoryInit}]", "Register.Do(appSettings.UnitTest);");
            }

            string appProgramTemplate = Resources.WebAppStartProgram
                .Replace("[{namespace}]", $"{project.Parameter.ProjectName}");


            string appControllerTemplate = Resources.WebAppStartHomeController
                .Replace("[{namespace}]", $"{project.Parameter.ProjectName}.Controllers");

            CreateFile appStartUp = new CreateFile(new CreateFileParam(project.Parameter, "Code", "Startup.cs") { Template = appStartUpTemplate });
            project.Add(appStartUp);

            CreateFile appProgram = new CreateFile(new CreateFileParam(project.Parameter, "Code", "Program.cs") { Template = appProgramTemplate });
            project.Add(appProgram);

            CreateFile appController = new CreateFile(new CreateFileParam(project.Parameter, "Controllers", "HomeController.cs") { Template = appControllerTemplate });
            project.Add(appController);

            return project;
        }

        public static CreateProject AddAppAngular(this CreateProject project)
        {
            CreateFile appMain = new CreateFile(new CreateFileParam(project.Parameter, "App", "main.ts") { Template = Resources.WebAngularMain });
            project.Add(appMain);

            CreateFile appPolyfill = new CreateFile(new CreateFileParam(project.Parameter, "App", "polyfills.ts") { Template = Resources.WebAngularPolyfills });
            project.Add(appPolyfill);

            CreateFile appVendor = new CreateFile(new CreateFileParam(project.Parameter, "App", "vendor.ts") { Template = Resources.WebAngularVendor });
            project.Add(appVendor);

            CreateFile appIndex = new CreateFile(new CreateFileParam(project.Parameter, "App", "index.html") { Template = Resources.WebAngularIndex });
            project.Add(appIndex);

            CreateFile appComponentStyle = new CreateFile(new CreateFileParam(project.Parameter, "App\\Components", "app.scss") { Template = Resources.WebAppComponentStyle });
            project.Add(appComponentStyle);

            CreateFile appComponentHtml = new CreateFile(new CreateFileParam(project.Parameter, "App\\Components", "app.html") { Template = Resources.WebAppComponentHTML });
            project.Add(appComponentHtml);

            CreateFile appComponentType = new CreateFile(new CreateFileParam(project.Parameter, "App\\Components", "app.component.ts") { Template = Resources.WebAppComponentType });
            project.Add(appComponentType);

            CreateFile appComponentIndex = new CreateFile(new CreateFileParam(project.Parameter, "App\\Components", "index.ts") { Template = Resources.WebAppComponentIndex });
            project.Add(appComponentIndex);

            return project;
        }

        public static CreateProject AddAppStyles(this CreateProject project)
        {
            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, "Styles", "app.theme.scss") { Template = Resources.WebAppThemeScss });
            project.Add(file);
            return project;
        }

        public static CreateProject AddAppSettings(this CreateProject project)
        {
            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, "Styles", "app.variables.scss") { Template = Resources.WebAppVariablesScss });
            project.Add(file);
            return project;
        }

        public static CreateProject AddGulp(this CreateProject project, bool simple)
        {
            string template = Resources.GulpFileSimple;

            if (!simple)
            {
                template = Resources.GulpFile
                    .Replace("[{entity}]", $"{project.Parameter.SolutionParam.SolutionName}.Business.Entity")
                    .Replace("[{resource}]", $"{project.Parameter.SolutionParam.SolutionName}.Business.Resource")
                    .Replace("[{codegenerator}]", $"{project.Parameter.SolutionParam.SolutionName}.CodeGenerator");
            }

            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "gulpfile.js") { Template = template });
            project.Add(file);
            return project;
        }

        public static CreateProject AddPackage(this CreateProject project)
        {
            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "package.json") { Template = Resources.PackageJson });
            project.Add(file);
            return project;
        }

        public static CreateProject AddTsConfig(this CreateProject project)
        {
            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "tsconfig.json") { Template = Resources.TsConfigJson });
            project.Add(file);
            return project;
        }

        public static CreateProject AddTypings(this CreateProject project)
        {
            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "typings.json") { Template = Resources.TypingsJson });
            project.Add(file);
            return project;
        }

        public static CreateProject AddWebConfig(this CreateProject project)
        {
            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "web.config") { Template = Resources.WebConfig });
            project.Add(file);
            return project;
        }

        public static CreateProject AddWebpackConfig(this CreateProject project)
        {
            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "webpack.config.js") { Template = Resources.WebpackConfig });
            project.Add(file);
            return project;
        }


    }
}
