using System.Collections.Generic;

namespace XCommon.CodeGeratorV2.Writer.CSharp
{
    public class Project
    {
        public Project(string name, ProjectType type, Solution solution)
        {
            Name = name;
            Solution = solution;
            Type = type;
            Classes = new List<Class>();
            Dependency = new List<string>();
        }

        public List<Class> Classes { get; set; }

        public List<string> Dependency { get; set; }

        public ProjectType Type { get; set; }

        public string Name { get; set; }

        public Solution Solution { get; set; }
    }
}
