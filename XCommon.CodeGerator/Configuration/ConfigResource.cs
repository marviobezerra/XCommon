using System;
using System.Collections.Generic;
using System.Resources;
using XCommon.Application;

namespace XCommon.CodeGerator.Configuration
{
	public class ConfigResource
    {
		public ConfigResource()
		{
			Cultures = new List<ApplicationCulture>();
			Resources = new Dictionary<Type, ResourceManager>();
		}

		public bool LazyLoad { get; set; }

		public string RequestAddress { get; set; }

		public string Path { get; set; }

		public string File { get; set; }

		public ApplicationCulture CultureDefault { get; set; }

		public List<ApplicationCulture> Cultures { get; set; }

		public Dictionary<Type, ResourceManager> Resources { get; set; }
	}
}
