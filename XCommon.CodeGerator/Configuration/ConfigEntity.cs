using System.Collections.Generic;
using System.Reflection;

namespace XCommon.CodeGerator.Configuration
{
	public class ConfigEntity
    {
		public ConfigEntity()
		{
			Assemblys = new List<Assembly>();
		}

		public string Path { get; set; }

		public List<Assembly> Assemblys { get; set; }

	}
}
