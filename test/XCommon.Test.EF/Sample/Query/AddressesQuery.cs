using System.Linq;
using XCommon.Extensions.Checks;
using XCommon.Patterns.Specification.Query;
using XCommon.Patterns.Specification.Query.Extensions;
using XCommon.Test.EF.Sample.Context;
using XCommon.Test.EF.Sample.Entity;

namespace XCommon.Test.EF.Sample.Query
{
	public class AddressesQuery : SpecificationQuery<Addresses, AddressesFilter>
	{
		public override IQueryable<Addresses> Build(IQueryable<Addresses> source, AddressesFilter filter)
		{
			var specifications = NewSpecificationList()
				.And(e => e.IdAddress == filter.Key, f => f.Key.HasValue)
				.And(e => filter.Keys.Contains(e.IdPerson), f => f.Keys.IsValidList())
				.OrderBy(e => e.StreetName)
				.Take(filter);

			return CheckSpecifications(specifications, source, filter);
		}
	}
}
