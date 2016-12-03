using XCommon.Patterns.Repository.Entity;
using XCommon.Application.Executes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XCommon.Patterns.Repository
{
    public interface IRepository<TEntity, TFilter>
        where TEntity : EntityBase, new()
        where TFilter : FilterBase, new()
    {
        Task<TEntity> GetNewAsync();
        Task<TEntity> GetByKeyAsync(Guid key);
        Task<List<TEntity>> GetByFilterAsync(TFilter filter);
        Task<Execute<TEntity>> SaveAsync(Execute<TEntity> execute);
        Task<Execute<List<TEntity>>> SaveManyAsync(Execute<List<TEntity>> execute);
        Task<Execute> ValidateAsync(TEntity entity);
        Task<Execute> ValidateManyAsync(List<TEntity> entity);
    }
}
