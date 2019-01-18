using XCommon.EF.Application.Context;
using XCommon.EF.Application.Context.System;
using XCommon.Entity.System;
using XCommon.Entity.System.Filter;
using XCommon.Patterns.Repository;

namespace XCommon.EF.Application.System.Implementation
{
	public class ConfigBusiness : RepositoryEFBase<ConfigEntity, ConfigFilter, Config, XCommonDbContext>
	{
	}
}
