using System;
using XCommon.Patterns.Specification.Entity.Implementation;
using XCommon.Util;
using XCommon.Extensions.String;

namespace XCommon.Patterns.Specification.Entity.Extensions
{
	public static class SpecificationCustom
	{
		public static SpecificationEntity<TEntity> AndIsEmail<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector)
			=> specification.AndIsEmail(selector, null, null);
		
		public static SpecificationEntity<TEntity> AndIsEmail<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
		{
			specification.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.Email, message, args));
			return specification;
		}

		public static SpecificationEntity<TEntity> AndIsCPF<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector)
			=> specification.AndIsCPF(selector, null, null);
		
		public static SpecificationEntity<TEntity> AndIsCPF<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
		{
			specification.Add(new AndIsValid<TEntity>(c => selector(c).CPFValido(), message, args));
			return specification;
		}

		public static SpecificationEntity<TEntity> AndIsCNPJ<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector)
			=> specification.AndIsCNPJ(selector, null, null);
		
		public static SpecificationEntity<TEntity> AndIsCNPJ<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
		{
			specification.Add(new AndIsValid<TEntity>(c => selector(c).CPNJValido(), message, args));
			return specification;
		}

		public static SpecificationEntity<TEntity> AndIsURL<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector)
			=> specification.AndIsURL(selector, null, null);
		
		public static SpecificationEntity<TEntity> AndIsURL<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
		{
			specification.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.URL, message, args));
			return specification;
		}

		public static SpecificationEntity<TEntity> AndIsPhone<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector)
			=> specification.AndIsPhone(selector, null, null);
		
		public static SpecificationEntity<TEntity> AndIsPhone<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
		{
			specification.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.Phone, message, args));
			return specification;
		}

		public static SpecificationEntity<TEntity> AndIsValidRegex<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, string regex)
			=> specification.AndIsValidRegex(selector, regex, null, null);
		
		public static SpecificationEntity<TEntity> AndIsValidRegex<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, string regex, string message, params object[] args)
		{
			specification.Add(new AndIsValidRegex<TEntity>(selector, regex, message, args));
			return specification;
		}
	}
}
