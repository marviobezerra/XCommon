using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommon.CodeGeratorV2.Configuration
{
    public class ConfigAngular
    {
        public string AppRoot { get; set; }
        public string ComponentPath { get; set; }
        public string HtmlRoot { get; set; }
        public string ServicePath { get; set; }
        public List<string> StyleInclude { get; set; }
    }
}
