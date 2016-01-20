using System;
using System.Linq.Expressions;
using XCommon.Patterns.Specification.Entity.Implementation;

namespace XCommon.Patterns.Specification.Entity.Extensions
{
    public static class SpecificationValueCheckBiggerThan
    {
        public static SpecificationEntity<TEntity> AndIsBiggerThan<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, int>> value, Expression<Func<TEntity, int>> compareTo)
        {
            return specification.AndIsBiggerThan(value, compareTo, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsBiggerThan<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, int>> value, Expression<Func<TEntity, int>> compareTo, string message, params object[] args)
        {
            specification.Add(new AndCheckValue<TEntity, int>(value, compareTo, null, AndCheckValueType.Int, AndCheckCompareType.BiggerThan, message, args));
            return specification;
        }

        public static SpecificationEntity<TEntity> AndIsBiggerThan<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, decimal>> value, Expression<Func<TEntity, decimal>> compareTo)
        {
            return specification.AndIsBiggerThan(value, compareTo, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsBiggerThan<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, decimal>> value, Expression<Func<TEntity, decimal>> compareTo, string message, params object[] args)
        {
            specification.Add(new AndCheckValue<TEntity, decimal>(value, compareTo, null, AndCheckValueType.Decimal, AndCheckCompareType.BiggerThan, message, args));
            return specification;
        }

        public static SpecificationEntity<TEntity> AndIsBiggerThan<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> compareTo)
        {
            return specification.AndIsBiggerThan(value, compareTo, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsBiggerThan<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> compareTo, string message, params object[] args)
        {
            specification.Add(new AndCheckValue<TEntity, DateTime>(value, compareTo, null, AndCheckValueType.Date, AndCheckCompareType.BiggerThan, message, args));
            return specification;
        }

        public static SpecificationEntity<TEntity> AndIsBiggerThan<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> compareTo, bool removeTime)
        {
            return specification.AndIsBiggerThan(value, compareTo, removeTime, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsBiggerThan<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> compareTo, bool removeTime, string message, params object[] args)
        {
            specification.Add(new AndCheckValue<TEntity, DateTime>(value, compareTo, null, AndCheckValueType.Date, AndCheckCompareType.BiggerThan, removeTime, message, args));
            return specification;
        }
    }
}
