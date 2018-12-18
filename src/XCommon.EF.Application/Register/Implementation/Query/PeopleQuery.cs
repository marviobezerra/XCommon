using System.Linq;
using XCommon.Application.Register.Entity.Filter;
using XCommon.EF.Application.Context.Register;
using XCommon.Extensions.Checks;
using XCommon.Extensions.String;
using XCommon.Patterns.Specification.Query;
using XCommon.Patterns.Specification.Query.Extensions;

namespace XCommon.EF.Application.Register.Implementation.Query
{
	public class PeopleQuery : SpecificationQuery<People, PeopleFilter>
	{
		public override IQueryable<People> Build(IQueryable<People> source, PeopleFilter filter)
		{
			var spefications = NewSpecificationList()
				.And(e => e.IdPerson == filter.Key, f => f.Key.HasValue)
				.And(e => filter.Keys.Contains(e.IdPerson), f => f.Keys.IsValidList())
				.And(e => e.Name.Contains(filter.Name), f => f.Name.IsNotEmpty())
				.And(e => e.Email == filter.Email, f => f.Email.IsNotEmpty())
				.OrderBy(e => e.Name)
				.Take(filter.PageNumber, filter.PageSize);

			return CheckSpecifications(spefications, source, filter);
		}
	}
}
