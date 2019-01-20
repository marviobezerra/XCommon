using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XCommon.Application.Executes;
using XCommon.EF.Application.Context;
using XCommon.EF.Application.Context.System;
using XCommon.EF.Application.System.Interface;
using XCommon.Entity.System;
using XCommon.Entity.System.Filter;
using XCommon.Patterns.Repository;
using XCommon.Patterns.Repository.Entity;

namespace XCommon.EF.Application.System.Implementation
{
	public class ConfigsBusiness : RepositoryEFBase<ConfigsEntity, ConfigsFilter, Configs, XCommonDbContext>, IConfigsBusiness
	{
		public async Task<Execute<TConfig>> Set<TModule, TConfig>(TModule module, string key, TConfig value)
		{
			var result = new Execute<TConfig>();
			var moduleName = ParseModuleName(module);
			var configuration = await GetFirstByFilterAsync(new ConfigsFilter { Module = moduleName, ConfigKey = key });

			if (configuration == null)
			{
				configuration = new ConfigsEntity
				{
					Action = EntityAction.New,
					IdConfig = Guid.NewGuid(),
					Module = moduleName,
					ConfigKey = key,
					Value = JsonConvert.SerializeObject(value)
				};
			}
			else
			{
				configuration.Action = EntityAction.Update;
				configuration.Value = JsonConvert.SerializeObject(value);
			}

			result.AddMessage(await SaveAsync(configuration));

			if (!result.HasErro)
			{
				result.Entity = value;
			}

			return result;
		}

		public async Task<Execute<TConfig>> Set<TModule, TConfig>(TModule module, TConfig value) 
			=> await Set(module, ParserConfigName<TConfig>(), value);

		public async Task<TConfig> Get<TModule, TConfig>(TModule module) 
			=> await Get<TModule, TConfig>(module, ParserConfigName<TConfig>());


		public async Task<TConfig> Get<TModule, TConfig>(TModule module, string key)
		{
			var moduleName = ParseModuleName(module);
			var configuration = await GetFirstByFilterAsync(new ConfigsFilter { Module = moduleName, ConfigKey = key });

			var result = configuration == null
				   ? GetDefault<TConfig>()
				   : JsonConvert.DeserializeObject<TConfig>(configuration.Value);

			return result;
		}

		protected virtual string ParseModuleName<TModule>(TModule module)
		{
			return module.ToString();
		}

		protected virtual string ParserConfigName<TConfig>()
		{
			return typeof(TConfig).Name
				.Replace("Entity", string.Empty)
				.ToLower();
		}

		protected virtual TConfig GetDefault<TConfig>()
		{
			if (typeof(TConfig).IsClass)
			{
				return Activator.CreateInstance<TConfig>();
			}

			return default(TConfig);
		}
	}
}
