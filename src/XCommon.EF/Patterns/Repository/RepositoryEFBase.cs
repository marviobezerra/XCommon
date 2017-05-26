using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCommon.Application.Executes;
using XCommon.Application.Logger;
using XCommon.Extensions.Converters;
using XCommon.Patterns.Ioc;
using XCommon.Patterns.Repository.Entity;
using XCommon.Patterns.Specification.Query;
using XCommon.Patterns.Specification.Validation;

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
		protected virtual ISpecificationQuery<TData, TFilter> SpecificationQuery { get; set; }

		[Inject(forceResolve: false)]
		protected virtual ILogger Logger { get; set; }

		protected virtual string GetEntityName()
		{
			return typeof(TEntity).Name.Replace("Entity", string.Empty);
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
			var execute = new Execute();

			foreach (var entity in entitys.Where(c => c.Action == EntityAction.Delete))
			{
				try
				{
					var dataEntity = entity.Convert<TData>();
					context.Entry<TData>(dataEntity).State = EntityState.Deleted;
				}
				catch (Exception ex)
				{
					execute.AddMessage(ex, "Error on delete info to: {0}", GetEntityName());
				}
			}

			foreach (var entity in entitys.Where(c => c.Action == EntityAction.New))
			{
				try
				{
					var dataEntity = entity.Convert<TData>();
					context.Entry<TData>(dataEntity).State = EntityState.Added;
				}
				catch (Exception ex)
				{
					execute.AddMessage(ex, "Error on insert info to: {0}", GetEntityName());
				}
			}

			foreach (var entity in entitys.Where(c => c.Action == EntityAction.Update))
			{
				try
				{
					var dataEntity = entity.Convert<TData>();
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

				foreach (var entity in entitys)
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
			var result = new List<TEntity>();

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
			using (var db = new TContext())
			{
				return await SaveAsync(execute, db);
			}
		}

		public virtual async Task<Execute<TEntity>> SaveAsync(Execute<TEntity> execute, DbContext context)
		{
			var executeList = new Execute<List<TEntity>>(execute)
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
			using (var db = new TContext())
			{
				return await SaveManyAsync(execute, db);
			}
		}

		public virtual async Task<Execute<List<TEntity>>> SaveManyAsync(Execute<List<TEntity>> execute, DbContext context)
		{
			try
			{
				execute.AddMessage(await ValidateManyAsync(execute.Entity));

				if (execute.HasErro)
				{
					return execute;
				}

				execute.AddMessage(await SaveAsync(execute.Entity, context));

				if (!execute.HasErro)
				{
					await AfterExecuteAsync(execute.Entity);
				}
			}
			catch (Exception ex)
			{
				execute.AddMessage(ex, string.Format("Erro on save: {0}", GetEntityName()));
			}

			return execute;
		}

		public async Task<Execute<TEntity>> SaveAsync(TEntity entity, DbContext context)
			=> await SaveAsync(new Execute<TEntity>(entity), context);

		public async Task<Execute<List<TEntity>>> SaveManyAsync(List<TEntity> entities, DbContext context)
			=> await SaveManyAsync(new Execute<List<TEntity>>(entities), context);

		public async Task<Execute<TEntity>> SaveAsync(TEntity entity)
			=> await SaveAsync(new Execute<TEntity>(entity));

		public async Task<Execute<List<TEntity>>> SaveManyAsync(List<TEntity> entities)
			=> await SaveManyAsync(new Execute<List<TEntity>>(entities));

		public virtual async Task<Execute> ValidateAsync(TEntity entity)
		{
			return await ValidateManyAsync(new List<TEntity> { entity });
		}

		public virtual async Task<Execute> ValidateManyAsync(List<TEntity> entity)
		{
			return await Task.Run(() =>
			{
				var result = new Execute();

				entity.ForEach(c => SpecificationValidate.IsSatisfiedBy(c, result));

				return result;
			});
		}
	}
}
