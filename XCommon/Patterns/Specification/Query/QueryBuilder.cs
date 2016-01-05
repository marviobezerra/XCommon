using XCommon.Patterns.Specification.Query.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace XCommon.Patterns.Specification.Query
{
    public class QueryBuilder<TEntity, TFilter>
    {
        private List<ISpecificationQuery<TEntity, TFilter>> Specifications { get; set; }

        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> Sort { get; set; }

        private int Page { get; set; }

        private int PageSize { get; set; }

        private bool PageApply { get; set; }

        public QueryBuilder()
        {
            Specifications = new List<ISpecificationQuery<TEntity, TFilter>>();
        }

        internal void AddSpecification(ISpecificationQuery<TEntity, TFilter> specification)
        {
            Specifications.Add(specification);
        }

        internal void AddOrder<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            if (Sort != null)
                Sort = items => Sort(items).ThenBy(property);
            else
                Sort = items => items.OrderBy(property);
        }

        public IQueryable<TEntity> Build(IEnumerable<TEntity> query, TFilter filter)
        {
            return Build(query.AsQueryable(), filter);
        }

        public IQueryable<TEntity> Build(IQueryable<TEntity> query, TFilter filter)
        {
            foreach (var specification in Specifications)
            {
                var predicate = specification.IsSatisfied();
                var condition = specification.CanApllyBy();

                if (predicate != null && condition.Compile().Invoke(filter))
                    query = query.Where(predicate);
            }

            if (Sort != null)
                query = Sort(query);

            if (PageApply)
                query = query.Skip((Page - 1) * PageSize).Take(PageSize);

            return query;
        }

        public QueryBuilder<TEntity, TFilter> And(Expression<Func<TEntity, bool>> predicate)
        {
            return And(predicate, c => true);
        }

        public QueryBuilder<TEntity, TFilter> And(Expression<Func<TEntity, bool>> predicate, Expression<Func<TFilter, bool>> conditon)
        {
            AddSpecification(new SpecificationQueryBase<TEntity, TFilter>(predicate, conditon));
            return this;
        }

        public QueryBuilder<TEntity, TFilter> OrderBy<TProperty>(Expression<Func<TEntity, TProperty>> predicate)
        {
            AddOrder(predicate);
            return this;
        }

        public QueryBuilder<TEntity, TFilter> Take(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
            PageApply = true;
            return this;
        }
    }
}
