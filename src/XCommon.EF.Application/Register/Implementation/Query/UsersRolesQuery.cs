using System.Linq;
using XCommon.EF.Application.Context.Register;
using XCommon.Entity.Register.Filter;
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
				.And(e => e.IdUserRole == filter.Key, f => f.Key.HasValue)
				.And(e => e.IdUser == filter.IdUser, f => f.IdUser.HasValue)
				.And(e => filter.Keys.Contains(e.IdUserRole), f => f.Keys.IsValidList())
				.And(e => filter.IdUsers.Contains(e.IdUser), f => f.IdUsers.IsValidList())
				.OrderBy(e => e.IdUser)
				.Take(filter.PageNumber, filter.PageSize);

			return CheckSpecifications(spefications, source, filter);
		}
	}
}
