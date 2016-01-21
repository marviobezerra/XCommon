using System;
using XCommon.Patterns.Specification.Entity.Implementation;

namespace XCommon.Patterns.Specification.Entity.Extensions
{
    public static class SpecificationUtils
    {
        public static SpecificationEntity<TEntity> AndIsValid<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, bool> selector, bool stopIfInvalid = false)
        {
            return specification.AndIsValid(selector, stopIfInvalid, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsValid<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, bool> selector, string message, params object[] args)
        {
            return specification.AndIsValid(selector, false, message, args);
        }

        public static SpecificationEntity<TEntity> AndIsValid<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, bool> selector, bool stopIfInvalid, string message, params object[] args)
        {
            specification.Add(new AndIsValid<TEntity>(selector, message, args), stopIfInvalid);
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
