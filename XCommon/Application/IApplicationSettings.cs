using System.Collections.Generic;

namespace XCommon.Application
{
	public interface IApplicationSettings
	{
		string Version { get; }

		string Name { get; }

		string ConnectionString { get; }

		bool UnitTest { get; }

		bool Production { get; }

		ApplicationCulture Culture { get; }

		List<ApplicationCulture> Cultures { get; }
	}

	public class ApplicationSettings : IApplicationSettings
	{
		public ApplicationSettings()
		{
			Cultures = new List<ApplicationCulture>();
		}

		public string ConnectionString { get; set; }

		public bool UnitTest { get; set; }

		public bool Production { get; set; }

		public ApplicationCulture Culture { get; set; }

		public List<ApplicationCulture> Cultures { get; set; }

		public string Name { get; set; }

		public string Version { get; set; }
	}

	public class ApplicationCulture
	{
		public string Name { get; set; }

		public string Description { get; set; }
	}
}
