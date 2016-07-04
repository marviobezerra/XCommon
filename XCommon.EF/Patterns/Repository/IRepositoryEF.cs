using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;
using XCommon.Patterns.Repository.Entity;
using XCommon.Patterns.Repository.Executes;

namespace XCommon.Patterns.Repository
{
    public interface IRepositoryEF<TEntity, TFilter> : IRepository<TEntity, TFilter>
        where TEntity : EntityBase, new()
        where TFilter : FilterBase, new()
    {
        Task<Execute<TEntity>> SaveAsync(Execute<TEntity> execute, DbContext context);
        Task<Execute<List<TEntity>>> SaveManyAsync(Execute<List<TEntity>> execute, DbContext context);
    }
}
