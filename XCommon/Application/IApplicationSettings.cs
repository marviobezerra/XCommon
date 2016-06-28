namespace XCommon.Application
{
	public interface IApplicationSettings
	{
		string ConnectionString { get; }

		bool UnitTest { get; }

		bool Production { get; }
	}

	public class ApplicationSettings : IApplicationSettings
	{
		public string ConnectionString { get; set; }

		public bool UnitTest { get; set; }

		public bool Production { get; set; }
	}
}
