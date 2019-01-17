using System;
using System.Collections.Generic;
using System.Text;
using XCommon.Entity.Register;
using XCommon.Entity.Register.Filter;
using XCommon.Patterns.Repository;

namespace XCommon.EF.Application.Register.Interface
{
	public interface IUsersRolesBusiness : IRepositoryEF<UsersRolesEntity, UsersRolesFilter>
	{
	}
}
