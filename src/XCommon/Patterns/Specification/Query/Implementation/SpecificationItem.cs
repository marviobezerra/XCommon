using System;
using System.Linq.Expressions;

namespace XCommon.Patterns.Specification.Query
{
    public class SpecificationItem<TEntity, TFilter>
    {
        public SpecificationItem(Expression<Func<TEntity, bool>> predicate, Func<TFilter, bool> condition)
        {
            Predicate = predicate;
            Condition = condition;
        }
        
        public Expression<Func<TEntity, bool>> Predicate { get; set; }

        public Func<TFilter, bool> Condition { get; set; }
    }
}
