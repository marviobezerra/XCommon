using System.Collections.Generic;

namespace XCommon.CodeGerator.Configuration
{
	public class ConfigAngular
    {
		public ConfigAngular()
		{
			StyleInclude = new List<string>();
			StyleMainExtra = new List<string>();
		}

		public string AppRoot { get; set; }

		public string StylePath { get; set; }

		public string StyleMain { get; set; }

		public List<string> StyleMainExtra { get; set; }

		public List<string> StyleInclude { get; set; }
		
		public string ComponentPath { get; set; }

		public string ServicePath { get; set; }		
	}
}
