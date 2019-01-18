using XCommon.Patterns.Repository.Entity;

namespace XCommon.Entity.Register.Filter
{
	public class PeopleFilter : FilterBase
	{
		public string Name { get; set; }

		public string Email { get; set; }
	}
}
