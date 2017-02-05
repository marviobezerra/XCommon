using System;
using XCommon.Patterns.Specification.Validation.Implementation;

namespace XCommon.Patterns.Specification.Validation.Extensions
{
    public static class SpecificationOr
    {
        public static SpecificationList<TEntity> Or<TEntity>(this SpecificationList<TEntity> specification, ISpecificationValidation<TEntity> spec1, ISpecificationValidation<TEntity> spec2, bool stopIfInvalid = false)
            => specification.Or(spec1, spec2, c => true, stopIfInvalid);

        public static SpecificationList<TEntity> Or<TEntity>(this SpecificationList<TEntity> specification, ISpecificationValidation<TEntity> spec1, ISpecificationValidation<TEntity> spec2, bool condition, bool stopIfInvalid = false)
            => specification.Or(spec1, spec2, c => condition, stopIfInvalid);

        public static SpecificationList<TEntity> Or<TEntity>(this SpecificationList<TEntity> specification, ISpecificationValidation<TEntity> spec1, ISpecificationValidation<TEntity> spec2, Func<TEntity, bool> condition, bool stopIfInvalid = false)
        {
            specification.Add(new Or<TEntity>(spec1, spec2, condition), stopIfInvalid);
            return specification;
        }
    }
}
