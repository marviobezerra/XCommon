using System.Linq;
using XCommon.Extensions.Checks;
using XCommon.Patterns.Specification.Query;
using XCommon.Patterns.Specification.Query.Extensions;
using XCommon.Test.EF.Sample.Context;
using XCommon.Test.EF.Sample.Entity;

namespace XCommon.Test.EF.Sample.Query
{

	public class PeopleQuery : SpecificationQuery<People, PeopleFilter>
	{
		public override IQueryable<People> Build(IQueryable<People> source, PeopleFilter filter)
		{
			var specifications = NewSpecificationList()
				.And(e => e.IdPerson == filter.Key, f => f.Key.HasValue)
				.And(e => filter.Keys.Contains(e.IdPerson), f => f.Keys.IsValidList())
				.OrderBy(e => e.Name)
				.Take(filter);

			return CheckSpecifications(specifications, source, filter);
		}
	}
}
