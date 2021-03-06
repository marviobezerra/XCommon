using XCommon.Entity.Register;
using XCommon.Entity.Register.Filter;
using XCommon.EF.Application.Context;
using XCommon.EF.Application.Context.Register;
using XCommon.Patterns.Repository;
using XCommon.EF.Application.Register.Interface;

namespace XCommon.EF.Application.Register.Implementation
{
	public class UsersBusiness : RepositoryEFBase<UsersEntity, UsersFilter, Users, XCommonDbContext>, IUsersBusiness
	{

	}
}
