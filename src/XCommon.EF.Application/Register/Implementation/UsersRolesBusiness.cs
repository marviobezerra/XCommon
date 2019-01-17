using XCommon.Entity.Register;
using XCommon.Entity.Register.Filter;
using XCommon.EF.Application.Context;
using XCommon.EF.Application.Context.Register;
using XCommon.Patterns.Repository;

namespace XCommon.EF.Application.Register.Implementation
{
	public class UsersRolesBusiness : RepositoryEFBase<UsersRolesEntity, UsersRolesFilter, UsersRoles, XCommonDbContext>
	{
	}
}