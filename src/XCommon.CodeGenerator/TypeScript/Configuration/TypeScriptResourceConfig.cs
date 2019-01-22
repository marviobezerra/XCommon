using System;
using System.Collections.Generic;
using System.Resources;
using XCommon.Application.Settings;

namespace XCommon.CodeGenerator.TypeScript.Configuration
{
	public class TypeScriptResourceConfig
    {
		public TypeScriptResourceConfig()
		{
			Cultures = new List<ApplicationCulture>();
			Resources = new Dictionary<Type, ResourceManager>();
		}

		public bool Extractor { get; set; }

		public bool Execute { get; set; }

		public string RequestAddress { get; set; }

		public string File { get; set; }

		public string Path { get; set; }

		public string JsonPrefix { get; set; }

		public string PathJson { get; set; }

		public string ServiceName { get; set; }

		public ApplicationCulture CultureDefault { get; set; }

		public List<ApplicationCulture> Cultures { get; set; }

		public Dictionary<Type, ResourceManager> Resources { get; set; }
	}
}
