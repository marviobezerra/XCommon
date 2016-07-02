using System.Collections.Generic;

namespace XCommon.CodeGerator.Configuration
{
	public class ConfigAngular
    {
		public ConfigAngular()
		{
            HtmlRoot = ".";
			StyleInclude = new List<string>();
		}

		public string AppRoot { get; set; }

		public List<string> StyleInclude { get; set; }
		
		public string ComponentPath { get; set; }

		public string ServicePath { get; set; }

        public string HtmlRoot { get; set; }
    }
}
