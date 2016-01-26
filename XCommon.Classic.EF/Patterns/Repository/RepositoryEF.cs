using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using XCommon.Application.ContextEF;
using XCommon.Extensions.Converters;
using XCommon.Extensions.String;
using XCommon.Patterns.Ioc;
using XCommon.Patterns.Repository.Entity;
using XCommon.Patterns.Repository.Executes;
using XCommon.Patterns.Specification.Entity;
using XCommon.Patterns.Specification.Query;

namespace XCommon.Patterns.Repository.Defaults
{
	public abstract class RepositoryEF<TEntity, TFilter, TData, TContext> : IRepository<TEntity, TFilter>
        where TEntity : EntityBase, new()
        where TFilter : FilterBase, new()
        where TData : class, new()
        where TContext : DbContext
    {
		
		protected virtual IContextFactory<TContext> Context => Kernel.Resolve<IContextFactory<TContext>>();
		
		protected virtual ISpecificationEntity<TEntity> SpecificationValidate => Kernel.Resolve<ISpecificationEntity<TEntity>>();

		protected virtual IQueryBuilder<TData, TFilter> SpecificationQuery => Kernel.Resolve<IQueryBuilder<TData, TFilter>>();

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

        protected virtual async Task<Execute> SaveAsync(TEntity entity)
        {
            return await SaveAsync(new List<TEntity> { entity });
        }

        protected async Task<Execute> SaveAsync(List<TEntity> entitys)
        {
            Execute execute = new Execute();

            using (DbContext db = await Context.CreateAsync())
            {
                foreach (TEntity entidade in entitys)
                {
                    TData dataEntity = entidade.Convert<TData>();

                    try
                    {
                        switch (entidade.Action)
                        {
                            case EntityAction.New:
                                db.Entry<TData>(dataEntity).State = EntityState.Added;
                                break;
                            case EntityAction.Update:
                                db.Entry<TData>(dataEntity).State = EntityState.Modified;
                                break;
                            case EntityAction.Delete:
                                db.Entry<TData>(dataEntity).State = EntityState.Deleted;
                                break;
                            case EntityAction.None:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        execute.AddMessage(ex, "Error on update info to: {0}", GetEntityName());
                    }
                }

                if (!execute.HasErro)
                    await db.SaveChangesAsync();
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
                    if (run != null)
                        run(entity);
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

			using (var db = await Context.CreateAsync())
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
            Execute<List<TEntity>> executeList = new Execute<List<TEntity>>(execute)
            {
                Entity = new List<TEntity> { execute.Entity }
            };

            executeList = await SaveManyAsync(executeList);

            execute.Entity = executeList.Entity.FirstOrDefault();
            execute.AddMessage(executeList);

            return execute;
        }

        public virtual async Task<Execute<List<TEntity>>> SaveManyAsync(Execute<List<TEntity>> execute)
        {
            try
            {
                execute.AddMessage(await ValidateManyAsync(execute.Entity));

                if (execute.HasErro)
                    return execute;

                execute.AddMessage(await SaveAsync(execute.Entity));

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
