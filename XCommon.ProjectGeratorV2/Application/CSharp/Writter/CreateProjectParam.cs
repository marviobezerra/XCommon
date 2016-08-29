using System;

namespace XCommon.ProjectGeratorV2.Application.CSharp.Writter
{
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
