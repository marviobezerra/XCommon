using System;
using XCommon.Patterns.Specification.Validation.Implementation;
using XCommon.Util;
using XCommon.Extensions.String;

namespace XCommon.Patterns.Specification.Validation.Extensions
{
	public static class SpecificationCustom
	{
		public static SpecificationValidation<TEntity> AndIsEmail<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, string> selector)
			=> specification.AndIsEmail(selector, null, null);
		
		public static SpecificationValidation<TEntity> AndIsEmail<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
		{
			specification.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.Email, message, args));
			return specification;
		}

		public static SpecificationValidation<TEntity> AndIsCPF<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, string> selector)
			=> specification.AndIsCPF(selector, null, null);
		
		public static SpecificationValidation<TEntity> AndIsCPF<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
		{
			specification.Add(new AndIsValid<TEntity>(c => selector(c).CPFValido(), message, args));
			return specification;
		}

		public static SpecificationValidation<TEntity> AndIsCNPJ<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, string> selector)
			=> specification.AndIsCNPJ(selector, null, null);
		
		public static SpecificationValidation<TEntity> AndIsCNPJ<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
		{
			specification.Add(new AndIsValid<TEntity>(c => selector(c).CPNJValido(), message, args));
			return specification;
		}

		public static SpecificationValidation<TEntity> AndIsURL<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, string> selector)
			=> specification.AndIsURL(selector, null, null);
		
		public static SpecificationValidation<TEntity> AndIsURL<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
		{
			specification.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.URL, message, args));
			return specification;
		}

		public static SpecificationValidation<TEntity> AndIsPhone<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, string> selector)
			=> specification.AndIsPhone(selector, null, null);
		
		public static SpecificationValidation<TEntity> AndIsPhone<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
		{
			specification.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.Phone, message, args));
			return specification;
		}

		public static SpecificationValidation<TEntity> AndIsValidRegex<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, string> selector, string regex)
			=> specification.AndIsValidRegex(selector, regex, null, null);
		
		public static SpecificationValidation<TEntity> AndIsValidRegex<TEntity>(this SpecificationValidation<TEntity> specification, Func<TEntity, string> selector, string regex, string message, params object[] args)
		{
			specification.Add(new AndIsValidRegex<TEntity>(selector, regex, message, args));
			return specification;
		}
	}
}
