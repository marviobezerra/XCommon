using XCommon.Patterns.Repository.Entity;
using XCommon.Patterns.Repository.Executes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace XCommon.Patterns.Repository.Defaults
{
    public abstract class RepositoryRest<TEntity, TFilter> : IRepository<TEntity, TFilter>
        where TEntity : EntityBase, new()
        where TFilter : FilterBase, new()
    {
        public string BaseAdress { get; private set; }
        public string User { get; private set; }
        public string Password { get; private set; }

        public RepositoryRest(string baseAdress)
            : this(baseAdress, string.Empty, string.Empty)
        {

        }

        public RepositoryRest(string baseAdress, string user, string password)
        {
            BaseAdress = baseAdress;
            User = user;
            Password = password;
        }

        protected TReturn GetRest<TReturn, TParam>(TParam param, string url)
        {
            using (HttpClient client = new HttpClient { BaseAddress = new Uri(this.BaseAdress) })
            {
                var response = client.PostAsJsonAsync(url, param).Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<TReturn>().Result;
                }

                throw new Exception(response.ToString());
            }
        }

        protected async Task<TReturn> GetRestAsync<TReturn, TParam>(TParam param, string url)
        {
            using (HttpClient client = new HttpClient { BaseAddress = new Uri(this.BaseAdress) })
            {
                var response = await client.PostAsJsonAsync(url, param);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<TReturn>();
                }

                throw new Exception(response.ToString());
            }
        }

        public TEntity GetByKey(Guid key)
        {
            return GetRest<TEntity, Guid>(key, "GetByKey");
        }

        public async Task<TEntity> GetByKeyAsync(Guid key)
        {
            return await GetRestAsync<TEntity, Guid>(key, "GetByKey");
        }

        public List<TEntity> GetByFilter(TFilter filter)
        {
            return GetRest<List<TEntity>, TFilter>(filter, "GetByFilter");
        }

        public async Task<List<TEntity>> GetByFilterAsync(TFilter filter)
        {
            return await GetRestAsync<List<TEntity>, TFilter>(filter, "GetByFilter");
        }

        public Execute<TEntity> Save(Execute<TEntity> execute)
        {
            return GetRest<Execute<TEntity>, Execute<TEntity>>(execute, "Save");
        }

        public async Task<Execute<TEntity>> SaveAsync(Execute<TEntity> execute)
        {
            return await GetRestAsync<Execute<TEntity>, Execute<TEntity>>(execute, "Save");
        }

        public Execute<List<TEntity>> SaveMany(Execute<List<TEntity>> execute)
        {
            return GetRest<Execute<List<TEntity>>, Execute<List<TEntity>>>(execute, "SaveMany");
        }

        public async Task<Execute<List<TEntity>>> SaveManyAsync(Execute<List<TEntity>> execute)
        {
            return await GetRestAsync<Execute<List<TEntity>>, Execute<List<TEntity>>>(execute, "SaveMany");
        }

        public bool CanDelete(Guid key)
        {
            return GetRest<bool, Guid>(key, "CanDelete");
        }

        public async Task<bool> CanDeleteAsync(Guid key)
        {
            return await GetRestAsync<bool, Guid>(key, "CanDelete");
        }

        public Execute Validate(TEntity entity)
        {
            return GetRest<Execute, TEntity>(entity, "Validate");
        }

        public async Task<Execute> ValidateAsync(TEntity entity)
        {
            return await GetRestAsync<Execute, TEntity>(entity, "Validate");
        }

        public Execute ValidateMany(List<TEntity> entity)
        {
            return GetRest<Execute, List<TEntity>>(entity, "ValidateMany");
        }

        public async Task<Execute> ValidateManyAsync(List<TEntity> entity)
        {
            return await GetRestAsync<Execute, List<TEntity>>(entity, "ValidateMany");
        }

        public TEntity GetNew()
        {
            return GetRest<TEntity, object>(null, "GetNew");
        }

        public async Task<TEntity> GetNewAsync()
        {
            return await GetRestAsync<TEntity, object>(null, "GetNew");
        }
    }
}
