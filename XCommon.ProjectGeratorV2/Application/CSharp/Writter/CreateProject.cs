using System.Collections.Generic;
using System.IO;
using XCommon.ProjectGeratorV2.Application.Commands;

namespace XCommon.ProjectGeratorV2.Application.CSharp.Writter
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
}
