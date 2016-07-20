using System.Collections.Generic;
using System.IO;
using XCommon.Application.ConsoleX;

namespace XCommon.ProjectGerator.Command
{
    public class CreateSolution : Command<CreateSolutionParam>
    {
        public CreateSolution(CreateSolutionParam param)
            : base(param)
        {
            Projects = new List<ICommand<CreateProjectParam>>();
        }

        protected override void Run(CreateSolutionParam param)
        {
            Console.WriteLineGreen($"  - Created solution: {param.SolutionName}");

            if (!Directory.Exists(param.Path))
                Directory.CreateDirectory(param.Path);

            string file = Path.Combine(param.Path, param.SolutionName + ".sln");
            string template = Process(Resources.CSharp.Solution);

            File.WriteAllText(file, template);

            Projects.ForEach(proj => proj.Run());
        }

        private List<ICommand<CreateProjectParam>> Projects { get; set; }

        public CreateSolution Add(ICommand<CreateProjectParam> project)
        {
            Projects.Add(project);

            return this;
        }

        private string Process(string template)
        {
            var projects = string.Empty;
            var links = string.Empty;

            string projectTemplate = "Project(\"{{{0}}}\") = \"{1}\", \"{2}\\{2}.xproj\", \"{{{3}}}\"\r\nEndProject\r\n";
            
            string link1 = "		{{{0}}}.Debug|Any CPU.Build.0 = Debug|Any CPU\r\n";
            string link2 = "		{{{0}}}.Release|Any CPU.Build.0 = Release|Any CPU\r\n";

            Projects.ForEach(proj => {

                projects += string.Format(projectTemplate, proj.Parameter.Id, proj.Parameter.ProjectName, proj.Parameter.ProjectName, proj.Parameter.IdRelationShip);
                links += string.Format(link1, proj.Parameter.IdRelationShip);
                links += string.Format(link2, proj.Parameter.IdRelationShip);

            });

            return template
                .Replace("[{projects}]", projects)
                .Replace("[{link}]", links);
        }
    }

    public class CreateSolutionParam
    {
        public string Path { get; set; }

        public string SolutionName { get; set; }
    }
}
