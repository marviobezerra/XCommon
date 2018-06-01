using System.Collections.Generic;
using XCommon.Application.Cache;
using XCommon.Application.Logger;

namespace XCommon.Application
{
	public interface IApplicationSettings
	{
		string Version { get; }

		string Name { get; }

		string DataBaseConnectionString { get; }

		bool UnitTest { get; }

		bool Production { get; }

		string Url { get; }

		List<string> Urls { get; }

		LogType Logger { get; }

		ApplicationCulture Culture { get; }

		List<ApplicationCulture> Cultures { get; }

		ICache Values { get; }

		ApplicationAuthentication Authentication { get; }

		CloudServicesKeys CloudKeys { get; }
	}
}
