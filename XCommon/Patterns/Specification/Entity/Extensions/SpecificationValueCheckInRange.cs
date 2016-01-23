using System;
using XCommon.Patterns.Specification.Entity.Implementation;

namespace XCommon.Patterns.Specification.Entity.Extensions
{
	public static class SpecificationValueCheckInRange
	{
		public static SpecificationEntity<TEntity> AndIsInRange<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, int> value, Func<TEntity, int> start, Func<TEntity, int> end)
			=> specification.AndIsInRange(value, start, end, null, null);

		public static SpecificationEntity<TEntity> AndIsInRange<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, int> value, Func<TEntity, int> start, Func<TEntity, int> end, string message, params object[] args)
		{
			specification.Add(new AndCheckValue<TEntity, int>(value, start, end, AndCheckValueType.Int, AndCheckCompareType.InRange, message, args));
			return specification;
		}

		public static SpecificationEntity<TEntity> AndIsInRange<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, decimal> value, Func<TEntity, decimal> start, Func<TEntity, decimal> end)
			=> specification.AndIsInRange(value, start, end, null, null);

		public static SpecificationEntity<TEntity> AndIsInRange<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, decimal> value, Func<TEntity, decimal> start, Func<TEntity, decimal> end, string message, params object[] args)
		{
			specification.Add(new AndCheckValue<TEntity, decimal>(value, start, end, AndCheckValueType.Decimal, AndCheckCompareType.InRange, message, args));
			return specification;
		}
		
		public static SpecificationEntity<TEntity> AndIsInRange<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, DateTime> value, Func<TEntity, DateTime> start, Func<TEntity, DateTime> end, bool removeTime = false)
			=> specification.AndIsInRange(value, start, end, removeTime, null, null);

		public static SpecificationEntity<TEntity> AndIsInRange<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, DateTime> value, Func<TEntity, DateTime> start, Func<TEntity, DateTime> end, bool removeTime, string message, params object[] args)
		{
			specification.Add(new AndCheckValue<TEntity, DateTime>(value, start, end, AndCheckValueType.Date, AndCheckCompareType.InRange, removeTime, message, args));
			return specification;
		}
	}
}
