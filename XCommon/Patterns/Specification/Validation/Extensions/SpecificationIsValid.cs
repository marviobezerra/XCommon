using System;
using XCommon.Patterns.Specification.Validation.Implementation;

namespace XCommon.Patterns.Specification.Validation.Extensions
{
    public static class SpecificationIsValid
    {
        public static SpecificationList<TEntity> AndIsValid<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, bool> selector)
            => specification.AndIsValid(selector, c => true, false, null, null);

        public static SpecificationList<TEntity> AndIsValid<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, bool> selector, string message, params object[] args)
            => specification.AndIsValid(selector, c => true, false, message, args);

        public static SpecificationList<TEntity> AndIsValid<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, bool> selector, Func<TEntity, bool> condition, string message, params object[] args)
            => specification.AndIsValid(selector, condition, false, message, args);

        public static SpecificationList<TEntity> AndIsValid<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, bool> selector, bool condition, string message, params object[] args)
            => specification.AndIsValid(selector, c => condition, false, message, args);

        public static SpecificationList<TEntity> AndIsValid<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, bool> selector, Func<TEntity, bool> condition, bool stopIfInvalid, string message = "", params object[] args)
        {
            specification.Add(new AndIsValid<TEntity>(selector, condition, message, args), stopIfInvalid);
            return specification;
        }
    }
}
