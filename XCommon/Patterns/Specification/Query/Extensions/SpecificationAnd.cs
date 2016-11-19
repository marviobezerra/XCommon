using System;
using System.Linq.Expressions;
using XCommon.Patterns.Specification.Query.Implementation;

namespace XCommon.Patterns.Specification.Query.Extensions
{
    public static class SpecificationAnd
    {
        public static SpecificationList<TEntity, TFilter> And<TEntity, TFilter>(this SpecificationList<TEntity, TFilter> specification, Expression<Func<TEntity, bool>> predicate, bool condition = true)
            => specification.And(predicate, f => condition);

        public static SpecificationList<TEntity, TFilter> And<TEntity, TFilter>(this SpecificationList<TEntity, TFilter> specification, Expression<Func<TEntity, bool>> predicate, Func<TFilter, bool> condition)
        {
            specification.Items.Add(new SpecificationItem<TEntity, TFilter>(predicate, condition));
            return specification;
        }
    }
}
