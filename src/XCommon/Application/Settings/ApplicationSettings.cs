using System.Collections.Generic;
using XCommon.Application.Cache;
using XCommon.Application.Cache.Implementations;
using XCommon.Application.Logger;

namespace XCommon.Application
{
	public class ApplicationSettings : IApplicationSettings
	{
		public ApplicationSettings()
		{
			Values = new CacheInMemory();
			Cultures = new List<ApplicationCulture>();
			CloudKeys = new CloudServicesKeys();
			Urls = new List<string>();
		}

		public string Version { get; set; }

		public string Name { get; set; }

		public string DataBaseConnectionString { get; set; }

		public bool UnitTest { get; set; }

		public bool Production { get; set; }

		public string Url { get; set; }

		public List<string> Urls { get; set; }

		public LogType Logger { get; set; }

		public ApplicationCulture Culture { get; set; }

		public List<ApplicationCulture> Cultures { get; set; }

		public ICache Values { get; private set; }

		public CloudServicesKeys CloudKeys { get; set; }
	}
}
