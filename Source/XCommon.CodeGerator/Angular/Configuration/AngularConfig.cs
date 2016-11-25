using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommon.CodeGerator.Angular.Configuration
{
    public class AngularConfig
    {
		public AngularConfig()
		{
			HtmlRoot = ".";
			StyleInclude = new List<string>();
		}

		public List<string> StyleInclude { get; set; }

		public string ComponentPath { get; set; }

		public string DirectivePath { get; set; }

		public string PipePath { get; set; }

		public string ServicePath { get; set; }

		public string HtmlRoot { get; set; }
	}
}
