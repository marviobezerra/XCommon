using System;
using System.Collections.Generic;
using System.Resources;
using XCommon.Application;

namespace XCommon.CodeGenerator.TypeScript.Configuration
{
	public class TypeScriptResourceConfig
    {
		public TypeScriptResourceConfig()
		{
			Cultures = new List<ApplicationCulture>();
			Resources = new List<Type>();
		}

		public bool Execute { get; set; }

		public string RequestAddress { get; set; }

		public string File { get; set; }

		public string Path { get; set; }

		public string PathJson { get; set; }

		public string ServiceName { get; set; }

		public ApplicationCulture CultureDefault { get; set; }

		public List<ApplicationCulture> Cultures { get; set; }

		public List<Type> Resources { get; set; }
	}
}
