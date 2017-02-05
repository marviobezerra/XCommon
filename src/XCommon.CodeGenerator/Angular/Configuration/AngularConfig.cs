using System.Collections.Generic;

namespace XCommon.CodeGenerator.Angular.Configuration
{
    public class AngularConfig
    {
		public AngularConfig()
		{
            ComponentHtmlRoot = ".";
			StyleInclude = new List<string>();
		}

		public List<string> StyleInclude { get; set; }

		public string AppPath { get; set; }

        public string ComponentHtmlRoot { get; set; }
	}
}
