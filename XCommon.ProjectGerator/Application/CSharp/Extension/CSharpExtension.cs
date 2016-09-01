using System.Collections.Generic;
using XCommon.ProjectGerator.Application.Commands;
using XCommon.ProjectGerator.Application.CSharp.Enumeration;
using XCommon.ProjectGerator.Application.CSharp.Writter;

namespace XCommon.ProjectGerator.Application.CSharp.Extension
{
    public static class CSharpExtension
    {
        public static CreateSolution AddCSharpSolution(this List<ICommand> command, string path, string name)
        {
            CreateSolution solution = new CreateSolution(new CreateSolutionParam { Path = path, SolutionName = name });
            command.Add(solution);

            return solution;
        }

        public static CreateProject AddCSharpProject(this CreateSolution solution, string name)
        {
            CreateProject project = new CreateProject(new CreateProjectParam(solution.Parameter, name) { Template = Resources.CSharp.ProjectClassXproj });
            solution.Add(project);
            return project;
        }

        public static CreateProject AddFile(this CreateProject project, string path, string name, string template)
        {
            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, path, name) { Template = template });
            project.Add(file);
            return project;
        }

        public static CreateProject AddCSharpProjectJson(this CreateProject project, ProjectJson type)
        {
            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "project.json") { Template = GetProjectJson(project, type) });
            project.Add(file);
            return project;
        }

        public static CreateProject AddCSharpFactoryDo(this CreateProject project)
        {
            string template = Resources.CSharp.FactoryRegister
                .Replace("[{namespace}]", project.Parameter.ProjectName);

            CreateFile file = new CreateFile(new CreateFileParam(project.Parameter, ".", "Register.cs") { Template = template });
            project.Add(file);
            return project;
        }

        private static string GetProjectJson(CreateProject project, ProjectJson type)
        {
            var template = string.Empty;
            List<string> references = new List<string>();

            switch (type)
            {
                case ProjectJson.BusinessCodeGenerator:
                    template = Resources.CSharp.ProjectJsonCodeGenerator;
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Contract");
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Entity");
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Resource");
                    break;
                case ProjectJson.BusinessConcrecte:
                    template = Resources.CSharp.ProjectJsonConcrecte;
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Contract");
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Data");
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Entity");
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Resource");
                    break;
                case ProjectJson.BusinessContract:
                    template = Resources.CSharp.ProjectJsonContract;
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Entity");
                    break;
                case ProjectJson.BusinessData:
                    template = Resources.CSharp.ProjectJsonData;
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Entity");
                    break;
                case ProjectJson.BusinessEntity:
                    template = Resources.CSharp.ProjectJsonEntity;
                    break;
                case ProjectJson.BusinessFactory:
                    template = Resources.CSharp.ProjectJsonFactory;
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Concrecte");
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Contract");
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Data");
                    references.Add($"{project.Parameter.SolutionParam.SolutionName}.Business.Entity");
                    break;
                case ProjectJson.BusinessResource:
                    template = Resources.CSharp.ProjectJsonResource;
                    break;
                case ProjectJson.ViewWebSimple:
                    template = Resources.CSharp.ProjectJsonEntity;
                    break;
                default:
                    break;
            }

            return CommandExtencionsCommon.MergeProjectJson(template, references);
        }
    }
}
