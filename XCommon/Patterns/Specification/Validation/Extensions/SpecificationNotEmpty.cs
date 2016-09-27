using System;
using XCommon.Patterns.Specification.Validation.Implementation;

namespace XCommon.Patterns.Specification.Validation.Extensions
{
	public static class SpecificationNotEmpty
	{
		public static SpecificationValidation<TEntity> AndIsNotEmpty<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, string> selector)
			=> specification.AndIsNotEmpty(selector, null, null);
		
		public static SpecificationValidation<TEntity> AndIsNotEmpty<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
		{
			specification.Add(new AndIsNotEmpty<TEntity, string>(selector, AndIsNotEmptyType.String, message, args));
			return specification;
		}

		public static SpecificationValidation<TEntity> AndIsNotEmpty<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, int?> selector)
			=> specification.AndIsNotEmpty(selector, null, null);
		
		public static SpecificationValidation<TEntity> AndIsNotEmpty<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, int?> selector, string message, params object[] args)
		{
			specification.Add(new AndIsNotEmpty<TEntity, int?>(selector, AndIsNotEmptyType.Int, message, args));
			return specification;
		}

		public static SpecificationValidation<TEntity> AndIsNotEmpty<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, decimal?> selector)
			=> specification.AndIsNotEmpty(selector, null, null);
		
		public static SpecificationValidation<TEntity> AndIsNotEmpty<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, decimal?> selector, string message, params object[] args)
		{
			specification.Add(new AndIsNotEmpty<TEntity, decimal?>(selector, AndIsNotEmptyType.Decimal, message, args));
			return specification;
		}

		public static SpecificationValidation<TEntity> AndIsNotEmpty<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, DateTime?> selector)
			=> specification.AndIsNotEmpty(selector, null, null);
		
		public static SpecificationValidation<TEntity> AndIsNotEmpty<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, DateTime?> selector, string message, params object[] args)
		{
			specification.Add(new AndIsNotEmpty<TEntity, DateTime?>(selector, AndIsNotEmptyType.Date, message, args));
			return specification;
		}

		public static SpecificationValidation<TEntity> AndIsNotEmpty<TEntity, TValue>(this SpecificationValidation<TEntity> specification, Func<TEntity, TValue> selector)
			=> specification.AndIsNotEmpty(selector, null, null);
		
		public static SpecificationValidation<TEntity> AndIsNotEmpty<TEntity, TValue>(this SpecificationValidation<TEntity> specification, Func<TEntity, TValue> selector, string message, params object[] args)
		{
			specification.Add(new AndIsNotEmpty<TEntity, TValue>(selector, AndIsNotEmptyType.Object, message, args));
			return specification;
		}
	}
}
