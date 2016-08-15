using System.Collections.Generic;
using XCommon.CodeGeratorV2.Writer.Base;

namespace XCommon.CodeGeratorV2.Writer.CSharp
{
    public class Solution : WriterBase
    {
        public Solution(string name, string path)
        {
            Name = name;
            Path = path;
            Projects = new List<Project>();
        }

        public List<Project> Projects { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }
    }
}
