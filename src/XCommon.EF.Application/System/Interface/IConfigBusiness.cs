using XCommon.Entity.System;
using XCommon.Entity.System.Filter;
using XCommon.Patterns.Repository;

namespace XCommon.EF.Application.System.Interface
{
	public interface IConfigBusiness : IRepositoryEF<ConfigEntity, ConfigFilter>
	{
	}
}
