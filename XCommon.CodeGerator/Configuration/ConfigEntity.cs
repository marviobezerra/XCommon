using System;
using System.Collections.Generic;
using System.Reflection;

namespace XCommon.CodeGerator.Configuration
{
	public class ConfigEntity
    {
		public ConfigEntity()
		{
			Assemblys = new List<Assembly>();
			TypesExtra = new List<Type>();
		}

        public bool IncludeEntityUtil { get; set; }

        public string Path { get; set; }

		public List<Assembly> Assemblys { get; set; }

		public List<Type> TypesExtra { get; set; }

	}
}
