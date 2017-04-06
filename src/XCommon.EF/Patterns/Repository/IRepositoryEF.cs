using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using XCommon.Application.Executes;
using XCommon.Patterns.Repository.Entity;

namespace XCommon.Patterns.Repository
{
	public interface IRepositoryEF<TEntity, TFilter> : IRepository<TEntity, TFilter>
        where TEntity : EntityBase, new()
        where TFilter : FilterBase, new()
    {
		Task<Execute<TEntity>> SaveAsync(TEntity entity, DbContext context);
		Task<Execute<List<TEntity>>> SaveManyAsync(List<TEntity> entities, DbContext context);
		Task<Execute<TEntity>> SaveAsync(Execute<TEntity> execute, DbContext context);
        Task<Execute<List<TEntity>>> SaveManyAsync(Execute<List<TEntity>> entities, DbContext context);
    }
}
