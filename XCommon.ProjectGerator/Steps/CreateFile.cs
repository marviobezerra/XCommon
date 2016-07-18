using System;
using XCommon.ProjectGerator.Command;
using System.IO;

namespace XCommon.ProjectGerator.Steps
{
    public class CreateFile : Command<CreateFileParam>
    {
        public CreateFile(CreateFileParam param) 
            : base(param)
        {
        }

        protected override void Run(CreateFileParam param)
        {
            if (!Directory.Exists(param.Path))
                Directory.CreateDirectory(param.Path);

            var file = Path.Combine(param.Path, param.FileName);

            File.WriteAllText(file, param.Template);

            Console.WriteLine($"\t\tCreateFile: {file}");
        }
    }

    public class CreateFileParam
    {
        public CreateFileParam(CreateProjectParam projectParam, string path, string fileName)
        {
            ProjectParam = projectParam;
            Path = System.IO.Path.Combine(projectParam.SolutionParam.Path, projectParam.ProjectName, path);
            FileName = fileName;
        }

        public CreateProjectParam ProjectParam { get; private set; }

        public string Path { get; private set; }

        public string FileName { get; private set; }

        public string Template { get; set; }
    }
}
