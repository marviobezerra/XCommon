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
        Task<TEntity> GetByKeyAsync(Guid key);
        Task<List<TEntity>> GetByFilterAsync(TFilter filter);
		Task<TEntity> GetFirstByFilterAsync(TFilter filter);

		Task<Execute<TEntity>> SaveAsync(TEntity entity);
		Task<Execute<List<TEntity>>> SaveManyAsync(List<TEntity> enntities);

        Task<Execute> ValidateAsync(TEntity entity);
        Task<Execute> ValidateManyAsync(List<TEntity> entity);
    }
}
