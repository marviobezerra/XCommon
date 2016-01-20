using System;
using System.Linq.Expressions;
using XCommon.Patterns.Specification.Entity.Implementation;

namespace XCommon.Patterns.Specification.Entity.Extensions
{
    public static class SpecificationUtils
    {
        public static SpecificationEntity<TEntity> AndIsValid<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, bool>> selector)
        {
            return specification.AndIsValid(selector, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsValid<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, bool>> selector, string message, params object[] args)
        {
            specification.Add(new AndIsValid<TEntity>(selector, message, args));
            return specification;
        }

        public static SpecificationEntity<TEntity> And<TEntity>(this SpecificationEntity<TEntity> specification, ISpecificationEntity<TEntity> spec)
        {
            specification.Add(spec);
            return specification;
        }

        public static SpecificationEntity<TEntity> Or<TEntity>(this SpecificationEntity<TEntity> specification, ISpecificationEntity<TEntity> spec1, ISpecificationEntity<TEntity> spec2)
        {
            specification.Add(new OrSpecification<TEntity>(spec1, spec2));
            return specification;
        }
    }
}
