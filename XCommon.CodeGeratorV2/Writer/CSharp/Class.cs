using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommon.CodeGeratorV2.Writer.CSharp
{
    public class Class
    {
        public Class(string name, string nameSpace, Project project)
        {
            Name = name;
            NameSpace = nameSpace;
            Project = project;
        }

        public string Name { get; set; }

        public string NameSpace { get; set; }

        public Project Project { get; set; }
    }
}
