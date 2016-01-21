using System;
using XCommon.Patterns.Specification.Entity.Implementation;

namespace XCommon.Patterns.Specification.Entity.Extensions
{
    public static class SpecificationLessThan
    {
        public static SpecificationEntity<TEntity> AndIsLessThan<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, int> value, Func<TEntity, int> compareTo)
        {
            return specification.AndIsLessThan(value, compareTo, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsLessThan<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, int> value, Func<TEntity, int> compareTo, string message, params object[] args)
        {
            specification.Add(new AndCheckValue<TEntity, int>(value, compareTo, null, AndCheckValueType.Int, AndCheckCompareType.LessThan, message, args));
            return specification;
        }

        public static SpecificationEntity<TEntity> AndIsLessThan<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, decimal> value, Func<TEntity, decimal> compareTo)
        {
            return specification.AndIsLessThan(value, compareTo, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsLessThan<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, decimal> value, Func<TEntity, decimal> compareTo, string message, params object[] args)
        {
            specification.Add(new AndCheckValue<TEntity, decimal>(value, compareTo, null, AndCheckValueType.Decimal, AndCheckCompareType.LessThan, message, args));
            return specification;
        }

        public static SpecificationEntity<TEntity> AndIsLessThan<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, DateTime> value, Func<TEntity, DateTime> compareTo)
        {
            return specification.AndIsLessThan(value, compareTo, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsLessThan<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, DateTime> value, Func<TEntity, DateTime> compareTo, string message, params object[] args)
        {
            specification.Add(new AndCheckValue<TEntity, DateTime>(value, compareTo, null, AndCheckValueType.Date, AndCheckCompareType.LessThan, message, args));
            return specification;
        }

        public static SpecificationEntity<TEntity> AndIsLessThan<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, DateTime> value, Func<TEntity, DateTime> compareTo, bool removeTime)
        {
            return specification.AndIsLessThan(value, compareTo, removeTime, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsLessThan<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, DateTime> value, Func<TEntity, DateTime> compareTo, bool removeTime, string message, params object[] args)
        {
            specification.Add(new AndCheckValue<TEntity, DateTime>(value, compareTo, null, AndCheckValueType.Date, AndCheckCompareType.LessThan, removeTime, message, args));
            return specification;
        }        
    }
}
