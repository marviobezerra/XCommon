using System;
using System.Linq;

namespace XCommon.Patterns.Specification.Query.Implementation
{
    public class SpecificationOrder<TEntity, TFilter>
    {
        public SpecificationOrder(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort, bool condition = true)
            : this(sort, f => condition)
        {
        }

        public SpecificationOrder(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort, Func<TFilter, bool> condition)
        {
            Sort = sort;
            Condition = condition;
        }

        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> Sort { get; set; }

        public Func<TFilter, bool> Condition { get; set; }
    }
}
