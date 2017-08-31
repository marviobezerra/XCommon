using System.Collections.Generic;
using XCommon.Application.Cache;
using XCommon.Application.Cache.Implementations;
using XCommon.Application.Logger;

namespace XCommon.Application
{
	public interface IApplicationSettings
	{
		string Version { get; }

		string Name { get; }

		string DataBaseConnectionString { get; }

		string StorageConnectionString { get; }

		bool UnitTest { get; }

		bool Production { get; }

		string Url { get; }

		List<string> Urls { get; }

		LogType Logger { get; }

		ApplicationCulture Culture { get; }

		List<ApplicationCulture> Cultures { get; }

		ICache Values { get; }
	}

	public class ApplicationSettings : IApplicationSettings
	{
		public ApplicationSettings()
		{
			Values = new CacheInMemory();
			Cultures = new List<ApplicationCulture>();
			Urls = new List<string>();
		}

		public string Version { get; set; }

		public string Name { get; set; }

		public string DataBaseConnectionString { get; set; }

		public string StorageConnectionString { get; set; }

		public bool UnitTest { get; set; }

		public bool Production { get; set; }

		public string Url { get; set; }

		public List<string> Urls { get; set; }

		public LogType Logger { get; set; }

		public ApplicationCulture Culture { get; set; }

		public List<ApplicationCulture> Cultures { get; set; }

		public ICache Values { get; private set; }
	}

	public class ApplicationCulture
	{
		public string Name { get; set; }

		public string Description { get; set; }
	}
}
