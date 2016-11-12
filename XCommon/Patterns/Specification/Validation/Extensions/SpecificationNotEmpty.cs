using System;
using XCommon.Patterns.Specification.Validation.Implementation;

namespace XCommon.Patterns.Specification.Validation.Extensions
{
    public static class SpecificationNotEmpty
    {
        public static SpecificationList<TEntity> AndIsNotEmpty<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, string> selector)
            => specification.AndIsNotEmpty(selector, c => true, false, "Property value is empty", null);

        public static SpecificationList<TEntity> AndIsNotEmpty<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
            => specification.AndIsNotEmpty(selector, c => true, false, message, args);

        public static SpecificationList<TEntity> AndIsNotEmpty<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, string> selector, Func<TEntity, bool> condition, string message, params object[] args)
            => specification.AndIsNotEmpty(selector, condition, false, message, args);

        public static SpecificationList<TEntity> AndIsNotEmpty<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, string> selector, bool condition, string message, params object[] args)
            => specification.AndIsNotEmpty(selector, c => condition, false, message, args);

        public static SpecificationList<TEntity> AndIsNotEmpty<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, string> selector, Func<TEntity, bool> condition, bool stopIfInvalid, string message = "", params object[] args)
        {
            specification.Add(new AndIsNotEmpty<TEntity, string>(selector, AndIsNotEmptyType.String, condition, message, args), stopIfInvalid);
            return specification;
        }
    }
}
