using XCommon.Application;
using XCommon.Application.ContextEF;
using XCommon.Extensions.Converters;
using XCommon.Extensions.String;
using XCommon.Patterns.Ioc;
using XCommon.Patterns.Repository.Entity;
using XCommon.Patterns.Repository.Executes;
using XCommon.Patterns.Specification.Query;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace XCommon.Patterns.Repository.Defaults
{
    public abstract class RepositoryEF<TEntity, TFilter, TData, TContext> : IRepository<TEntity, TFilter>
        where TEntity : EntityBase, new()
        where TFilter : FilterBase, new()
        where TData : class, new()
        where TContext : DbContext
    {
        public RepositoryEF()
        {
            Kernel.Resolve(this);
        }

        #region Protected
        [Inject]
        protected IContextFactory<TContext> Context { get; set; }
        
        protected virtual void AfterExecute<TBase>(TBase entity)
            where TBase : EntityBase
        {
            if (entity.Action != EntityAction.Delete)
                entity.Action = EntityAction.None;
        }

        protected virtual void AfterExecute<TBase>(List<TBase> entitys)
            where TBase : EntityBase
        {
            AfterExecute(entitys, null);
        }

        protected virtual void AfterExecute<TBase>(List<TBase> entitys, Action<TBase> run)
            where TBase : EntityBase
        {
            entitys.RemoveAll(c => c.Action == EntityAction.Delete);

            foreach (TBase entity in entitys)
            {
                entity.Action = EntityAction.None;
                if (run != null)
                    run(entity);
            }
        }

        protected virtual string GetEntityName()
        {
            return typeof(TEntity).Name.Remove("Entity");
        }

        protected virtual string GetEntitySchema()
        {
            var nameSplit = typeof(TEntity).FullName.Split('.');
            return nameSplit[nameSplit.Count() - 1].Remove(".");
        }

        protected virtual string GetEntitySchemaName()
        {
            return string.Format("{0}.{1}", GetEntitySchema(), GetEntityName());
        }

        protected virtual TFilter GetDefaultFilterForKey(Guid key)
        {
            return new TFilter { Id = key };
        }

        protected virtual Execute Save(TEntity entity)
        {
            return Save(new List<TEntity> { entity });
        }

        protected Execute Save(List<TEntity> entitys)
        {
            Execute execute = new Execute();

            using (DbContext db = Context.Create())
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
                        execute.AddMessage(ex, "Ocorreu um erro ao atualizar as informações: {0}", GetEntityName());
                    }
                }

                if (!execute.HasErro)
                    db.SaveChanges();
            }

            return execute;
        }

        protected virtual QueryBuilder<TEntity, TFilter> GetQueryBuilder(TFilter filter)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Abstract
        public abstract List<TEntity> GetByFilter(TFilter filter);
        public abstract Execute ValidateMany(List<TEntity> entity);
        #endregion

        #region Public

        public virtual TEntity GetNew()
        {
            return new TEntity { Action = EntityAction.New, Key = Guid.NewGuid() };
        }

        public virtual TEntity GetByKey(Guid key)
        {
            var filter = GetDefaultFilterForKey(key);

            var lista = GetByFilter(filter);

            if (lista.Count <= 0)
                return null;

            return lista[0];
        }

        public virtual Execute<TEntity> Save(Execute<TEntity> execute)
        {
            Execute<List<TEntity>> executeList = new Execute<List<TEntity>>(execute)
            {
                Entity = new List<TEntity> { execute.Entity }
            };

            executeList = SaveMany(executeList);
            execute.AddMessage(executeList);

            return execute;
        }

        public virtual Execute<List<TEntity>> SaveMany(Execute<List<TEntity>> execute)
        {
            try
            {
                execute.AddMessage(ValidateMany(execute.Entity));

                if (execute.HasErro)
                    return execute;

                execute.AddMessage(Save(execute.Entity));

                if (!execute.HasErro)
                    AfterExecute(execute.Entity);
            }
            catch (Exception ex)
            {
                execute.AddMessage(ex, string.Format("Erro ao salvar: {0}", GetEntityName()));
            }

            return execute;
        }

        public virtual bool CanDelete(Guid key)
        {
            return true;
        }

        public virtual Execute Validate(TEntity entity)
        {
            return ValidateMany(new List<TEntity> { entity });
        }
        #endregion

        #region Public Async

        public virtual async Task<TEntity> GetNewAsync()
        {
            return await Task.Factory.StartNew(() => GetNew());
        }

        public virtual async Task<TEntity> GetByKeyAsync(Guid key)
        {
            return await Task.Factory.StartNew(() => GetByKey(key));
        }

        public virtual async Task<List<TEntity>> GetByFilterAsync(TFilter filter)
        {
            return await Task.Factory.StartNew(() => GetByFilter(filter));
        }

        public virtual async Task<Execute<TEntity>> SaveAsync(Execute<TEntity> execute)
        {
            return await Task.Factory.StartNew(() => Save(execute));
        }

        public virtual async Task<Execute<List<TEntity>>> SaveManyAsync(Execute<List<TEntity>> execute)
        {
            return await Task.Factory.StartNew(() => SaveMany(execute));
        }

        public virtual async Task<bool> CanDeleteAsync(Guid key)
        {
            return await Task.Factory.StartNew(() => CanDelete(key));
        }

        public virtual async Task<Execute> ValidateAsync(TEntity entity)
        {
            return await Task.Factory.StartNew(() => Validate(entity));
        }

        public virtual async Task<Execute> ValidateManyAsync(List<TEntity> entity)
        {
            return await Task.Factory.StartNew(() => ValidateMany(entity));
        }
        #endregion
    }
}
