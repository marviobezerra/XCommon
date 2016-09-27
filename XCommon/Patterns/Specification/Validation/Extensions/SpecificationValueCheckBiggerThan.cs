﻿using System;
using XCommon.Patterns.Specification.Validation.Implementation;

namespace XCommon.Patterns.Specification.Validation.Extensions
{
	public static class SpecificationValueCheckBiggerThan
	{
		public static SpecificationValidation<TEntity> AndIsBiggerThan<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, int> value, Func<TEntity, int> compareTo)
			=> specification.AndIsBiggerThan(value, compareTo, null, null);
		
		public static SpecificationValidation<TEntity> AndIsBiggerThan<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, int> value, Func<TEntity, int> compareTo, string message, params object[] args)
		{
			specification.Add(new AndCheckValue<TEntity, int>(value, compareTo, null, AndCheckValueType.Int, AndCheckCompareType.BiggerThan, message, args));
			return specification;
		}

		public static SpecificationValidation<TEntity> AndIsBiggerThan<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, decimal> value, Func<TEntity, decimal> compareTo)
			=> specification.AndIsBiggerThan(value, compareTo, null, null);
		
		public static SpecificationValidation<TEntity> AndIsBiggerThan<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, decimal> value, Func<TEntity, decimal> compareTo, string message, params object[] args)
		{
			specification.Add(new AndCheckValue<TEntity, decimal>(value, compareTo, null, AndCheckValueType.Decimal, AndCheckCompareType.BiggerThan, message, args));
			return specification;
		}

		public static SpecificationValidation<TEntity> AndIsBiggerThan<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, DateTime> value, Func<TEntity, DateTime> compareTo, bool removeTime = false)
			=> specification.AndIsBiggerThan(value, compareTo, removeTime, null, null);
		
		public static SpecificationValidation<TEntity> AndIsBiggerThan<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, DateTime> value, Func<TEntity, DateTime> compareTo, bool removeTime, string message, params object[] args)
		{
			specification.Add(new AndCheckValue<TEntity, DateTime>(value, compareTo, null, AndCheckValueType.Date, AndCheckCompareType.BiggerThan, removeTime, message, args));
			return specification;
		}
	}
}