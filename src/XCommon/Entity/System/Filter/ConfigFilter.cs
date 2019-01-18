using XCommon.Patterns.Repository.Entity;

namespace XCommon.Entity.System.Filter
{
	public class ConfigFilter : FilterBase
	{
		public string Section { get; set; }

		public string ConfigKey { get; set; }
	}
}
