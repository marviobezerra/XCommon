using System;
using System.Threading.Tasks;
using XCommon.Patterns.Specification.Validation.Implementation;

namespace XCommon.Patterns.Specification.Validation.Extensions
{
    public static class SpecificationIsValidAsync
    {
        public static SpecificationList<TEntity> AndIsValidAsync<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, Task<bool>> selector)
            => specification.AndIsValidAsync(selector, c => true, false, "Invalid value", null);

        public static SpecificationList<TEntity> AndIsValidAsync<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, Task<bool>> selector, string message, params object[] args)
            => specification.AndIsValidAsync(selector, c => true, false, message, args);

        public static SpecificationList<TEntity> AndIsValidAsync<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, Task<bool>> selector, Func<TEntity, bool> condition, string message, params object[] args)
            => specification.AndIsValidAsync(selector, condition, false, message, args);

        public static SpecificationList<TEntity> AndIsValidAsync<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, Task<bool>> selector, bool condition, string message, params object[] args)
            => specification.AndIsValidAsync(selector, c => condition, false, message, args);

        public static SpecificationList<TEntity> AndIsValidAsync<TEntity>(this SpecificationList<TEntity> specification, Func<TEntity, Task<bool>> selector, Func<TEntity, bool> condition, bool stopIfInvalid, string message = "", params object[] args)
        {
            specification.Add(new AndIsValidAsync<TEntity>(selector, condition, message, args), stopIfInvalid);
            return specification;
        }
    }
}
