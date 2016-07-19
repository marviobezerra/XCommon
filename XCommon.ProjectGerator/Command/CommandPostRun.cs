using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCommon.ProjectGerator.Command
{
    public class CommandPostRun
    {
        public string Name { get; set; }

        public string Arguments { get; internal set; }

        public string Command { get; internal set; }

        public string Directory { get; internal set; }
    }
}
