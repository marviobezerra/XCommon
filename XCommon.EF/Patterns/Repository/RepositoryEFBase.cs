using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCommon.Extensions.Converters;
using XCommon.Extensions.String;
using XCommon.Patterns.Ioc;
using XCommon.Patterns.Repository.Entity;
using XCommon.Patterns.Repository.Executes;
using XCommon.Patterns.Specification.Validation;
using XCommon.Patterns.Specification.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;
using XCommon.Application.Logger;

namespace XCommon.Patterns.Repository
{
    public abstract class RepositoryEFBase<TEntity, TFilter, TData, TContext> : IRepositoryEF<TEntity, TFilter>
        where TEntity : EntityBase, new()
        where TFilter : FilterBase, new()
        where TData : class, new()
        where TContext : DbContext, new()
    {
        public RepositoryEFBase()
        {
            Kernel.Resolve(this);
        }

        [Inject]
        protected virtual ISpecificationValidation<TEntity> SpecificationValidate { get; set; }

        [Inject]
        protected virtual IQueryBuilder<TData, TFilter> SpecificationQuery { get; set; }

        [Inject(forceResolve: false)]
        protected virtual ILogger Logger { get; set; }

        protected virtual string GetEntityName()
        {
            return typeof(TEntity).Name.Remove("Entity");
        }

        protected virtual TFilter GetDefaultFilterForKey(Guid key)
        {
            return new TFilter
            {
                Key = key
            };
        }
        
        protected virtual async Task<Execute> SaveAsync(List<TEntity> entitys, DbContext context)
        {
            Execute execute = new Execute();

            foreach (TEntity entidade in entitys.Where(c => c.Action == EntityAction.Delete))
            {
                try
                {
                    TData dataEntity = entidade.Convert<TData>();
                    context.Entry<TData>(dataEntity).State = EntityState.Deleted;
                }
                catch (Exception ex)
                {
                    execute.AddMessage(ex, "Error on delete info to: {0}", GetEntityName());
                }
            }

            foreach (TEntity entidade in entitys.Where(c => c.Action == EntityAction.New))
            {
                try
                {
                    TData dataEntity = entidade.Convert<TData>();
                    context.Entry<TData>(dataEntity).State = EntityState.Added;
                }
                catch (Exception ex)
                {
                    execute.AddMessage(ex, "Error on insert info to: {0}", GetEntityName());
                }
            }

            foreach (TEntity entidade in entitys.Where(c => c.Action == EntityAction.Update))
            {
                try
                {
                    TData dataEntity = entidade.Convert<TData>();
                    context.Entry<TData>(dataEntity).State = EntityState.Modified;
                }
                catch (Exception ex)
                {
                    execute.AddMessage(ex, "Error on update info to: {0}", GetEntityName());
                }
            }

            if (!execute.HasErro)
            {
                try
                {
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    execute.AddMessage(ex, "Error on savechanges in: {0}", GetEntityName());
                }
            }


            return execute;
        }

        protected virtual async Task AfterExecuteAsync<TBase>(List<TBase> entitys)
            where TBase : EntityBase
        {
            await AfterExecuteAsync(entitys, null);
        }

        protected virtual async Task AfterExecuteAsync<TBase>(List<TBase> entitys, Action<TBase> run)
            where TBase : EntityBase
        {
            await Task.Run(() =>
            {
                entitys.RemoveAll(c => c.Action == EntityAction.Delete);

                foreach (TBase entity in entitys)
                {
                    entity.Action = EntityAction.None;
					run?.Invoke(entity);
				}
            });
        }

        public virtual async Task<TEntity> GetNewAsync()
        {
            return await Task.Run(() =>
            {
                return new TEntity
                {
                    Action = EntityAction.New,
                    Key = Guid.NewGuid()
                };
            });
        }

        public virtual async Task<TEntity> GetByKeyAsync(Guid key)
        {
            var result = await GetByFilterAsync(GetDefaultFilterForKey(key));
            return result.FirstOrDefault();
        }

        public virtual async Task<List<TEntity>> GetByFilterAsync(TFilter filter)
        {
            List<TEntity> result = new List<TEntity>();

            using (var db = new TContext())
            {
                var query = SpecificationQuery.Build(db.Set<TData>(), filter);
                await query.ForEachAsync(data =>
                {
                    result.Add(data.Convert<TEntity>());
                });
            }

            return result;
        }

        public virtual async Task<Execute<TEntity>> SaveAsync(Execute<TEntity> execute)
        {
            using (TContext db = new TContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var result = await SaveAsync(execute, db);

                    if (!result.HasErro)
                        transaction.Commit();

                    return result;
                }
            }
        }

        public virtual async Task<Execute<TEntity>> SaveAsync(Execute<TEntity> execute, DbContext context)
        {
            Execute<List<TEntity>> executeList = new Execute<List<TEntity>>(execute)
            {
                Entity = new List<TEntity> { execute.Entity }
            };

            executeList = await SaveManyAsync(executeList, context);

            execute.Entity = executeList.Entity.FirstOrDefault();
            execute.AddMessage(executeList);

            return execute;
        }

        public virtual async Task<Execute<List<TEntity>>> SaveManyAsync(Execute<List<TEntity>> execute)
        {
            using (TContext db = new TContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var result = await SaveManyAsync(execute, db);

                    if (!result.HasErro)
                        transaction.Commit();

                    return result;
                }
            }
        }

        public virtual async Task<Execute<List<TEntity>>> SaveManyAsync(Execute<List<TEntity>> execute, DbContext context)
        {
            try
            {
                execute.AddMessage(await ValidateManyAsync(execute.Entity));

                if (execute.HasErro)
                    return execute;

                execute.AddMessage(await SaveAsync(execute.Entity, context));

                if (!execute.HasErro)
                    await AfterExecuteAsync(execute.Entity);
            }
            catch (Exception ex)
            {
                execute.AddMessage(ex, string.Format("Erro on save: {0}", GetEntityName()));
            }

            return execute;
        }

        public virtual async Task<Execute> ValidateAsync(TEntity entity)
        {
            return await ValidateManyAsync(new List<TEntity> { entity });
        }

        public virtual async Task<Execute> ValidateManyAsync(List<TEntity> entity)
        {
            return await Task.Run(() =>
            {
                Execute result = new Execute();

                entity.ForEach(c => SpecificationValidate.IsSatisfiedBy(c, result));

                return result;
            });
        }
    }
}
