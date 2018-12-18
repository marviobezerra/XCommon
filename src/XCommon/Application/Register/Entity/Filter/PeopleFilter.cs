using XCommon.Patterns.Repository.Entity;

namespace XCommon.Application.Register.Entity.Filter
{
	public class PeopleFilter : FilterBase
	{
		public string Name { get; set; }

		public string Email { get; set; }
	}
}
