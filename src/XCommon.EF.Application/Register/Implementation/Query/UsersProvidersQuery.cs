using System.Linq;
using XCommon.Application.Register.Entity.Filter;
using XCommon.EF.Application.Context.Register;
using XCommon.Extensions.Checks;
using XCommon.Patterns.Specification.Query;
using XCommon.Patterns.Specification.Query.Extensions;

namespace XCommon.EF.Application.Register.Implementation.Query
{
	public class UsersProvidersQuery : SpecificationQuery<UsersProviders, UsersProvidersFilter>
	{
		public override IQueryable<UsersProviders> Build(IQueryable<UsersProviders> source, UsersProvidersFilter filter)
		{
			var spefications = NewSpecificationList()
				.And(e => e.IdUserProvide == filter.Key, f => f.Key.HasValue)
				.And(e => e.IdUser == filter.IdUser, f => f.IdUser.HasValue)
				.And(e => e.Provider == filter.Provider, f => f.Provider.HasValue)
				.And(e => filter.Keys.Contains(e.IdUserProvide), f => f.Keys.IsValidList())
				.OrderBy(e => e.IdUserProvide)
				.Take(filter.PageNumber, filter.PageSize);

			return CheckSpecifications(spefications, source, filter);
		}
	}
}
