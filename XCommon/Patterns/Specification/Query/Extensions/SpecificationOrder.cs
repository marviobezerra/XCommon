using System;
using System.Linq;
using System.Linq.Expressions;
using XCommon.Patterns.Specification.Query.Implementation;

namespace XCommon.Patterns.Specification.Query.Extensions
{
    public static class SpecificationOrder
    {
        public static SpecificationList<TEntity, TFilter> OrderBy<TEntity, TFilter, TProperty>(this SpecificationList<TEntity, TFilter> specification, Expression<Func<TEntity, TProperty>> property, bool condition = true, bool descending = false)
            => specification.OrderBy(property, f => condition, descending);

        public static SpecificationList<TEntity, TFilter> OrderBy<TEntity, TFilter, TProperty>(this SpecificationList<TEntity, TFilter> specification, Expression<Func<TEntity, TProperty>> property, Func<TFilter, bool> condition, bool descending = false)
        {
            if (descending)
                specification.Order.Add(new SpecificationOrder<TEntity, TFilter>(items => items.OrderByDescending(property), condition));
            else
                specification.Order.Add(new SpecificationOrder<TEntity, TFilter>(items => items.OrderBy(property), condition));

            return specification;
        }
    }
}
