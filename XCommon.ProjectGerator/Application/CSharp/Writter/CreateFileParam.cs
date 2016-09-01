namespace XCommon.ProjectGerator.Application.CSharp.Writter
{
    public class CreateFileParam
    {
        public CreateFileParam(CreateProjectParam projectParam, string path, string fileName)
        {
            ProjectParam = projectParam;
            Path = System.IO.Path.Combine(projectParam.SolutionParam.Path, projectParam.ProjectName, path);
            FileName = fileName;
        }

        public CreateFileParam(string basePath, string path, string fileName)
        {
            Path = System.IO.Path.Combine(basePath, path);
            FileName = fileName;
        }

        public CreateProjectParam ProjectParam { get; private set; }

        public string Path { get; private set; }

        public string FileName { get; private set; }

        public string Template { get; set; }
    }
}
