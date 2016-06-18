using System.Collections.Generic;

namespace XCommon.CodeGerator.Configuration
{
	public class ConfigDataBase
    {
		public ConfigDataBase()
		{
			Remap = new List<ConfigDataBaseRemap>();
			Rewrite = new List<ConfigDataBaseRewrite>();
		}

		public string ContextName { get; set; }

		public string DataPath { get; set; }

		public string DataNameSpace { get; set; }

		public string ConnectionString { get; set; }

		public List<ConfigDataBaseRemap> Remap { get; set; }

		public List<ConfigDataBaseRewrite> Rewrite { get; set; }
	}
}
