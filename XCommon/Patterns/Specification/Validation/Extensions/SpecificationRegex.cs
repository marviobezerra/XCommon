using System;
using XCommon.Patterns.Specification.Validation.Implementation;

namespace XCommon.Patterns.Specification.Validation.Extensions
{
    public static class SpecificationRegex
    {
        public static SpecificationList<TEntity> AndRegexValid<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, string> selector, string expression, string message, params object[] args)
            => specification.AndRegexValid(selector, expression, c => true, false, message, args);

        public static SpecificationList<TEntity> AndRegexValid<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, string> selector, string expression, bool condition, bool stopIfInvalid, string message, params object[] args)
            => specification.AndRegexValid(selector, expression, c => condition, stopIfInvalid, message, args);

        public static SpecificationList<TEntity> AndRegexValid<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, string> selector, string expression, Func<TEntity, bool> condition, bool stopIfInvalid, string message, params object[] args)
        {
            specification.Add(new AndIsValidRegex<TEntity>(selector, expression, condition, message, args), stopIfInvalid);
            return specification;
        }
    }
}
