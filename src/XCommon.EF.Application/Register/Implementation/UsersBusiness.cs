using XCommon.Application.Register.Entity;
using XCommon.Application.Register.Entity.Filter;
using XCommon.EF.Application.Context;
using XCommon.EF.Application.Context.Register;
using XCommon.Patterns.Repository;

namespace XCommon.EF.Application.Register.Implementation
{
	public class UsersBusiness : RepositoryEFBase<UsersEntity, UsersFilter, Users, XCommonDbContext>
	{
	}
}
