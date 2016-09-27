using System;
using XCommon.Patterns.Specification.Validation.Implementation;

namespace XCommon.Patterns.Specification.Validation.Extensions
{
	public static class SpecificationUtils
	{
		public static SpecificationValidation<TEntity> AndIsValid<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, bool> selector, bool stopIfInvalid = false)
			=> specification.AndIsValid(selector, stopIfInvalid, null, null);

		public static SpecificationValidation<TEntity> AndIsValid<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, bool> selector, string message, params object[] args)
			=> specification.AndIsValid(selector, false, message, args);

		public static SpecificationValidation<TEntity> AndIsValid<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, bool> selector, bool stopIfInvalid, string message, params object[] args)
		{
			specification.Add(new AndIsValid<TEntity>(selector, message, args), stopIfInvalid);
			return specification;
		}

		public static SpecificationValidation<TEntity> And<TEntity>(this SpecificationValidation<TEntity> specification, ISpecificationValidation<TEntity> spec)
		{
			specification.Add(spec);
			return specification;
		}

		public static SpecificationValidation<TEntity> Or<TEntity>(this SpecificationValidation<TEntity> specification, ISpecificationValidation<TEntity> spec1, ISpecificationValidation<TEntity> spec2)
		{
			specification.Add(new OrSpecification<TEntity>(spec1, spec2));
			return specification;
		}
	}
}
