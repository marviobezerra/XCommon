using XCommon.ProjectGerator.Command;
using System;
using System.Collections.Generic;
using System.IO;

namespace XCommon.ProjectGerator.Steps
{
    public class CreateProject : Command<CreateProjectParam>
    {
        public CreateProject(CreateProjectParam param) 
            : base(param)
        {
            Files = new List<ICommand<CreateFileParam>>();
        }

        protected override void Run(CreateProjectParam param)
        {
            Console.WriteLine($"\tCreateProject {param.SolutionParam.SolutionName}.{param.ProjectName}");

            string path = Path.Combine(param.SolutionParam.Path, param.ProjectName);
            string file = Path.Combine(path, param.ProjectName + ".xproj");
            string content = string.Format(param.Template, param.Id, param.ProjectName);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            File.WriteAllText(file, content);

            Files.ForEach(projectFile => projectFile.Run());
        }

        private List<ICommand<CreateFileParam>> Files { get; set; }

        public CreateProject Add(ICommand<CreateFileParam> file)
        {
            Files.Add(file);
            return this;
        }

    }

    public class CreateProjectParam
    {
        public CreateProjectParam(CreateSolutionParam solutionParam, string projectName)
        {
            Id = Guid.NewGuid();
            SolutionParam = solutionParam;
            ProjectName = $"{solutionParam.SolutionName}.{projectName}";
        }

        public Guid Id { get; set; }



        public CreateSolutionParam SolutionParam { get; private set; }

        public string ProjectName { get; private set; }

        public string Template { get; set; }
    }
}
