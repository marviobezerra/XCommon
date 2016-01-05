using System;
using System.Linq.Expressions;

namespace XCommon.Patterns.Specification.Query.Implementation
{
    public class SpecificationQueryBase<TEntity, TFilter> : ISpecificationQuery<TEntity, TFilter>
    {
        private Expression<Func<TEntity, bool>> Predicate { get; set; }
        private Expression<Func<TFilter, bool>> Condition { get; set; }

        public SpecificationQueryBase(Expression<Func<TEntity, bool>> predicate, Expression<Func<TFilter, bool>> condition)
        {
            Predicate = predicate;
            Condition = condition;
        }

        public Expression<Func<TFilter, bool>> CanApllyBy()
        {
            return Condition;
        }

        public Expression<Func<TEntity, bool>> IsSatisfied()
        {
            return Predicate;
        }
    }
}
