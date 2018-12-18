using XCommon.Application.Register.Entity;
using XCommon.Application.Register.Entity.Filter;
using XCommon.Patterns.Repository;

namespace XCommon.EF.Application.Register.Interface
{
	public interface IUsersProvidersBusiness : IRepositoryEF<UsersProvidersEntity, UsersProvidersFilter>
	{
	}
}
