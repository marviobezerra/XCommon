using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XCommon.Patterns.Specification.Entity.Implementation;

namespace XCommon.Patterns.Specification.Entity.Extensions
{
    public static class SpecificationValueCheckInRange
    {
        public static SpecificationEntity<TEntity> AndIsInRange<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, int>> value, Expression<Func<TEntity, int>> start, Expression<Func<TEntity, int>> end)
        {
            return specification.AndIsInRange(value, start, end, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsInRange<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, int>> value, Expression<Func<TEntity, int>> start, Expression<Func<TEntity, int>> end, string message, params object[] args)
        {
            specification.Add(new AndCheckValue<TEntity, int>(value, start, end, AndCheckValueType.Int, AndCheckCompareType.InRange, message, args));
            return specification;
        }

        public static SpecificationEntity<TEntity> AndIsInRange<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, decimal>> value, Expression<Func<TEntity, decimal>> start, Expression<Func<TEntity, decimal>> end)
        {
            return specification.AndIsInRange(value, start, end, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsInRange<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, decimal>> value, Expression<Func<TEntity, decimal>> start, Expression<Func<TEntity, decimal>> end, string message, params object[] args)
        {
            specification.Add(new AndCheckValue<TEntity, decimal>(value, start, end, AndCheckValueType.Decimal, AndCheckCompareType.InRange, message, args));
            return specification;
        }

        public static SpecificationEntity<TEntity> AndIsInRange<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> start, Expression<Func<TEntity, DateTime>> end)
        {
            return specification.AndIsInRange(value, start, end, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsInRange<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> start, Expression<Func<TEntity, DateTime>> end, string message, params object[] args)
        {
            specification.Add(new AndCheckValue<TEntity, DateTime>(value, start, end, AndCheckValueType.Date, AndCheckCompareType.InRange, message, args));
            return specification;
        }

        public static SpecificationEntity<TEntity> AndIsInRange<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> start, Expression<Func<TEntity, DateTime>> end, bool removeTime)
        {
            return specification.AndIsInRange(value, start, end, removeTime, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsInRange<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> start, Expression<Func<TEntity, DateTime>> end, bool removeTime, string message, params object[] args)
        {
            specification.Add(new AndCheckValue<TEntity, DateTime>(value, start, end, AndCheckValueType.Date, AndCheckCompareType.InRange, removeTime, message, args));
            return specification;
        }
    }
}
