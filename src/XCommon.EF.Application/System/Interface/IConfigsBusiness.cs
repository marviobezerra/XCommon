using System.Threading.Tasks;
using XCommon.Application.Executes;
using XCommon.Entity.System;
using XCommon.Entity.System.Filter;
using XCommon.Patterns.Repository;

namespace XCommon.EF.Application.System.Interface
{
	public interface IConfigsBusiness : IRepositoryEF<ConfigsEntity, ConfigsFilter>
	{
		Task<Execute<TConfig>> Set<TModule, TConfig>(TModule module, TConfig value);
		Task<Execute<TConfig>> Set<TModule, TConfig>(TModule module, string key, TConfig value);

		Task<TConfig> Get<TModule, TConfig>(TModule module);
		Task<TConfig> Get<TModule, TConfig>(TModule module, string key);
	}
}
