using System;
using XCommon.Patterns.Specification.Entity.Implementation;

namespace XCommon.Patterns.Specification.Entity.Extensions
{
    public static class SpecificationNotEmpty
    {
        public static SpecificationEntity<TEntity> AndIsNotEmpty<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, bool stopIfInvalid = false)
        {
            return specification.AndIsNotEmpty(selector, stopIfInvalid, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsNotEmpty<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
        {
            return specification.AndIsNotEmpty(selector, false, message, args);
        }

        public static SpecificationEntity<TEntity> AndIsNotEmpty<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, bool stopIfInvalid, string message, params object[] args)
        {
            specification.Add(new AndIsNotEmpty<TEntity, string>(selector, AndIsNotEmptyType.String, message, args), stopIfInvalid);
            return specification;
        }

        public static SpecificationEntity<TEntity> AndIsNotEmpty<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, int?> selector, bool stopIfInvalid = false)
        {
            return specification.AndIsNotEmpty(selector, stopIfInvalid, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsNotEmpty<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, int?> selector, string message, params object[] args)
        {
            return specification.AndIsNotEmpty(selector, false, message, args);
        }

        public static SpecificationEntity<TEntity> AndIsNotEmpty<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, int?> selector, bool stopIfInvalid, string message, params object[] args)
        {
            specification.Add(new AndIsNotEmpty<TEntity, int?>(selector, AndIsNotEmptyType.Int, message, args), stopIfInvalid);
            return specification;
        }

        public static SpecificationEntity<TEntity> AndIsNotEmpty<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, decimal?> selector, bool stopIfInvalid = false)
        {
            return specification.AndIsNotEmpty(selector, stopIfInvalid, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsNotEmpty<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, decimal?> selector, string message, params object[] args)
        {
            return specification.AndIsNotEmpty(selector, false, message, args);
        }

        public static SpecificationEntity<TEntity> AndIsNotEmpty<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, decimal?> selector, bool stopIfInvalid, string message, params object[] args)
        {
            specification.Add(new AndIsNotEmpty<TEntity, decimal?>(selector, AndIsNotEmptyType.Decimal, message, args), stopIfInvalid);
            return specification;
        }

        public static SpecificationEntity<TEntity> AndIsNotEmpty<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, DateTime?> selector, bool stopIfInvalid = false)
        {
            return specification.AndIsNotEmpty(selector, stopIfInvalid, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsNotEmpty<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, DateTime?> selector, string message, params object[] args)
        {
            return specification.AndIsNotEmpty(selector, false, message, args);
        }

        public static SpecificationEntity<TEntity> AndIsNotEmpty<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, DateTime?> selector, bool stopIfInvalid, string message, params object[] args)
        {
            specification.Add(new AndIsNotEmpty<TEntity, DateTime?>(selector, AndIsNotEmptyType.Date, message, args), stopIfInvalid);
            return specification;
        }

        public static SpecificationEntity<TEntity> AndIsNotEmpty<TEntity, TValue>(this SpecificationEntity<TEntity> specification, Func<TEntity, TValue> selector, bool stopIfInvalid = false)
        {
            return specification.AndIsNotEmpty(selector, stopIfInvalid, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsNotEmpty<TEntity, TValue>(this SpecificationEntity<TEntity> specification, Func<TEntity, TValue> selector, string message, params object[] args)
        {
            return specification.AndIsNotEmpty(selector, false, message, args);
        }

        public static SpecificationEntity<TEntity> AndIsNotEmpty<TEntity, TValue>(this SpecificationEntity<TEntity> specification, Func<TEntity, TValue> selector, bool stopIfInvalid, string message, params object[] args)
        {
            specification.Add(new AndIsNotEmpty<TEntity, TValue>(selector, AndIsNotEmptyType.Object, message, args), stopIfInvalid);
            return specification;
        }
    }
}
