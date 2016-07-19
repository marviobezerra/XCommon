using System;
using System.Collections.Generic;
using System.IO;

namespace XCommon.ProjectGerator.Command
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
            Console.WriteLine($"    -> Created project: {param.ProjectName}");

            string file = Path.Combine(param.Path, param.ProjectName + ".xproj");
            string content = string.Format(param.Template, param.Id, param.ProjectName);

            if (!Directory.Exists(param.Path))
                Directory.CreateDirectory(param.Path);

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
            IdRelationShip = Guid.NewGuid();
            SolutionParam = solutionParam;
            ProjectName = $"{solutionParam.SolutionName}.{projectName}";
            Path = System.IO.Path.Combine(solutionParam.Path, ProjectName);
        }

        public string Path { get; set; }

        public Guid Id { get; set; }

        public Guid IdRelationShip { get; set; }

        public CreateSolutionParam SolutionParam { get; private set; }

        public string ProjectName { get; private set; }
        
        public string Template { get; set; }
    }
}
