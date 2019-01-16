using System.Linq;
using XCommon.Application.Register.Entity.Filter;
using XCommon.EF.Application.Context.Register;
using XCommon.Extensions.Checks;
using XCommon.Patterns.Specification.Query;
using XCommon.Patterns.Specification.Query.Extensions;

namespace XCommon.EF.Application.Register.Implementation.Query
{
	public class UsersRolesQuery : SpecificationQuery<UsersRoles, UsersRolesFilter>
	{
		public override IQueryable<UsersRoles> Build(IQueryable<UsersRoles> source, UsersRolesFilter filter)
		{
			var spefications = NewSpecificationList()
				.And(e => e.IdUser == filter.Key, f => f.Key.HasValue)
				.And(e => filter.Keys.Contains(e.IdUser), f => f.Keys.IsValidList())
				.OrderBy(e => e.IdUser)
				.Take(filter.PageNumber, filter.PageSize);

			return CheckSpecifications(spefications, source, filter);
		}
	}
}
