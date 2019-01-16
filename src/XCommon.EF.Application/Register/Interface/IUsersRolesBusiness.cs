using System;
using System.Collections.Generic;
using System.Text;
using XCommon.Application.Register.Entity;
using XCommon.Application.Register.Entity.Filter;
using XCommon.Patterns.Repository;

namespace XCommon.EF.Application.Register.Interface
{
	public interface IUsersRolesBusiness : IRepositoryEF<UsersRolesEntity, UsersRolesFilter>
	{
	}
}
