using XCommon.Patterns.Repository.Entity;

namespace XCommon.Entity.System.Filter
{
	public class ConfigsFilter : FilterBase
	{
		public string Module { get; set; }

		public string ConfigKey { get; set; }
	}
}
