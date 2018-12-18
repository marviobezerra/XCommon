using XCommon.Application.Register.Entity;
using XCommon.Application.Register.Entity.Filter;
using XCommon.Patterns.Repository;

namespace XCommon.EF.Application.Register.Interface
{
	public interface IUsersBusiness : IRepositoryEF<UsersEntity, UsersFilter>
	{
	}
}
