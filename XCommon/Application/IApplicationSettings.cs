using System;
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

        string Culture { get; }

        List<string> Cultures { get; }
	}

	public class ApplicationSettings : IApplicationSettings
	{
        public ApplicationSettings()
        {
            Cultures = new List<string>();
        }

		public string ConnectionString { get; set; }

		public bool UnitTest { get; set; }

		public bool Production { get; set; }

        public string Culture { get; set; }

        public List<string> Cultures { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }
    }
}
