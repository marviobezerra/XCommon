using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XCommon.Application.Executes;
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

		#region Inject
		[Inject]
		protected virtual ISpecificationValidation<TEntity> SpecificationValidate { get; set; }

		[Inject]
		protected virtual ISpecificationQuery<TData, TFilter> SpecificationQuery { get; set; }
		#endregion

		#region Internal
		protected virtual string GetEntityName()
		{
			return typeof(TEntity).Name.Replace("Entity", string.Empty);
		}
		#endregion

		#region Get
		public virtual async Task<TEntity> GetByKeyAsync(Guid key)
			=> await GetFirstByFilterAsync(new TFilter { Key = key });

		public virtual async Task<TEntity> GetFirstByFilterAsync(TFilter filter)
			=> (await GetByFilterAsync(filter)).FirstOrDefault();

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


		#endregion

		#region Save

		protected virtual async Task<Execute> BeforeSaveAsync(List<TEntity> entities)
		{
			return await Task.FromResult(new Execute());
		}

		protected virtual async Task<Execute> AfterSaveAsync(List<TEntity> entities)
		{
			return await Task.Run(() =>
			{
				entities.RemoveAll(c => c.Action == EntityAction.Delete);
				return new Execute();
			});
		}

		protected virtual async Task<Execute> SaveInternalAsync(List<TEntity> entities, DbContext context)
		{
			var execute = new Execute();

			// Call Before Save
			await BeforeSaveAsync(entities);

			// Validate the enties
			execute.AddMessage(await ValidateManyAsync(entities));

			// Something wrong? STOP!
			if (execute.HasErro)
			{
				return execute;
			}

			// Attach deleted entities to the DBContext
			foreach (var entity in entities.Where(c => c.Action == EntityAction.Delete))
			{
				try
				{
					var dataEntity = entity.Convert<TData>();
					context.Entry(dataEntity).State = EntityState.Deleted;
				}
				catch (Exception ex)
				{
					execute.AddException(ex, "Error on delete info to: {0}", GetEntityName());
				}
			}

			// Attach new entities to the DBContext
			foreach (var entity in entities.Where(c => c.Action == EntityAction.New))
			{
				try
				{
					var dataEntity = entity.Convert<TData>();
					context.Entry(dataEntity).State = EntityState.Added;
				}
				catch (Exception ex)
				{
					execute.AddException(ex, "Error on insert info to: {0}", GetEntityName());
				}
			}

			// Attach updated entities to the DBContext
			foreach (var entity in entities.Where(c => c.Action == EntityAction.Update))
			{
				try
				{
					var dataEntity = entity.Convert<TData>();
					context.Entry(dataEntity).State = EntityState.Modified;
				}
				catch (Exception ex)
				{
					execute.AddException(ex, "Error on update info to: {0}", GetEntityName());
				}
			}

			// If it's okay so far, try to save it and call after save.
			if (!execute.HasErro)
			{
				try
				{
					await context.SaveChangesAsync();
					await AfterSaveAsync(entities);
				}
				catch (Exception ex)
				{
					execute.AddException(ex, "Error on savechanges in: {0}", GetEntityName());
				}
			}


			return execute;
		}

		public async Task<Execute<TEntity>> SaveAsync(TEntity entity)
		{
			using (var db = new TContext())
			{
				return await SaveAsync(entity, db);
			}
		}

		public virtual async Task<Execute<TEntity>> SaveAsync(TEntity entity, DbContext context)
		{
			var resultList = await SaveManyAsync(new List<TEntity> { entity }, context);
			var result = new Execute<TEntity>(resultList)
			{
				Entity = resultList.Entity.FirstOrDefault()
			};

			return result;
		}

		public virtual async Task<Execute<List<TEntity>>> SaveManyAsync(List<TEntity> entities)
		{
			using (var db = new TContext())
			{
				return await SaveManyAsync(entities, db);
			}
		}

		public virtual async Task<Execute<List<TEntity>>> SaveManyAsync(List<TEntity> entities, DbContext context)
		{
			var result = new Execute<List<TEntity>>
			{
				Entity = entities
			};

			var saveResult = await SaveInternalAsync(result.Entity, context);

			result.AddMessage(saveResult);
			return result;
		}
		#endregion

		#region Validate
		public virtual async Task<Execute> ValidateAsync(TEntity entity)
			=> await ValidateManyAsync(new List<TEntity> { entity });

		public virtual async Task<Execute> ValidateManyAsync(List<TEntity> entity)
		{
			var result = new Execute();

			foreach (var item in entity)
			{
				await SpecificationValidate.IsSatisfiedByAsync(item, result);
			}

			return result;
		}
		#endregion
	}
}
