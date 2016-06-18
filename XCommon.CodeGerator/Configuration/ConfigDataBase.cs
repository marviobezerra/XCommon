using System.Collections.Generic;

namespace XCommon.CodeGerator.Configuration
{
	public class ConfigDataBase
    {
		public ConfigDataBase()
		{
			Remap = new List<ConfigDataBaseRemap>();
		}

		public string ConnectionString { get; set; }

		public List<ConfigDataBaseRemap> Remap { get; set; }
	}
}
