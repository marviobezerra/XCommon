using XCommon.ProjectGerator.Command;
using System;
using System.Collections.Generic;
using System.IO;

namespace XCommon.ProjectGerator.Steps
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
            Console.WriteLine($"CreateSolution: {param.SolutionName}");

            if (!Directory.Exists(param.Path))
                Directory.CreateDirectory(param.Path);

            string file = Path.Combine(param.Path, param.SolutionName + ".sln");

            File.WriteAllText(file, Properties.Resources.Solution);

            Projects.ForEach(proj => proj.Run());
        }

        private List<ICommand<CreateProjectParam>> Projects { get; set; }

        public CreateSolution Add(ICommand<CreateProjectParam> project)
        {
            Projects.Add(project);

            return this;
        }
    }

    public class CreateSolutionParam
    {
        public string Path { get; set; }

        public string SolutionName { get; set; }
    }
}
