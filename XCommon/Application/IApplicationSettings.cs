namespace XCommon.Application
{
	public interface IApplicationSettings
	{
		string ConnectionString { get; set; }

		bool UnitTest { get; set; }
	}

	public class ApplicationSettings : IApplicationSettings
	{
		public string ConnectionString { get; set; }

		public bool UnitTest { get; set; }
	}
}
