using System;
using XCommon.Patterns.Specification.Entity.Implementation;
using XCommon.Util;

namespace XCommon.Patterns.Specification.Entity.Extensions
{
	public static class SpecificationCustom
	{
		public static SpecificationEntity<TEntity> AndIsEmail<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, bool stopIfInvalid = false)
			=> specification.AndIsEmail(selector, stopIfInvalid, null, null);

		public static SpecificationEntity<TEntity> AndIsEmail<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
			=> specification.AndIsEmail(selector, false, message, args);

		public static SpecificationEntity<TEntity> AndIsEmail<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, bool stopIfInvalid, string message, params object[] args)
		{
			specification.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.Email, message, args), stopIfInvalid);
			return specification;
		}

		public static SpecificationEntity<TEntity> AndIsCPF<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, bool stopIfInvalid = false)
			=> specification.AndIsCPF(selector, stopIfInvalid, null, null);

		public static SpecificationEntity<TEntity> AndIsCPF<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
			=> specification.AndIsCPF(selector, false, message, args);

		public static SpecificationEntity<TEntity> AndIsCPF<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, bool stopIfInvalid, string message, params object[] args)
		{
			specification.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.CPF, message, args), stopIfInvalid);
			return specification;
		}

		public static SpecificationEntity<TEntity> AndIsCNPJ<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, bool stopIfInvalid = false)
			=> specification.AndIsCNPJ(selector, stopIfInvalid, null, null);

		public static SpecificationEntity<TEntity> AndIsCNPJ<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
			=> specification.AndIsCNPJ(selector, false, message, args);

		public static SpecificationEntity<TEntity> AndIsCNPJ<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, bool stopIfInvalid, string message, params object[] args)
		{
			specification.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.CNPJ, message, args), stopIfInvalid);
			return specification;
		}

		public static SpecificationEntity<TEntity> AndIsURL<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, bool stopIfInvalid = false)
			=> specification.AndIsURL(selector, stopIfInvalid, null, null);

		public static SpecificationEntity<TEntity> AndIsURL<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
			=> specification.AndIsURL(selector, false, message, args);

		public static SpecificationEntity<TEntity> AndIsURL<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, bool stopIfInvalid, string message, params object[] args)
		{
			specification.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.URL, message, args), stopIfInvalid);
			return specification;
		}

		public static SpecificationEntity<TEntity> AndIsPhone<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, bool stopIfInvalid = false)
			=> specification.AndIsPhone(selector, stopIfInvalid, null, null);

		public static SpecificationEntity<TEntity> AndIsPhone<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, string message, params object[] args)
			=> specification.AndIsPhone(selector, false, message, args);

		public static SpecificationEntity<TEntity> AndIsPhone<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, bool stopIfInvalid, string message, params object[] args)
		{
			specification.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.Phone, message, args), stopIfInvalid);
			return specification;
		}

		public static SpecificationEntity<TEntity> AndIsValidRegex<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, string regex, bool stopIfInvalid = false)
			=> specification.AndIsValidRegex(selector, regex, stopIfInvalid, null, null);

		public static SpecificationEntity<TEntity> AndIsValidRegex<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, string regex, string message, params object[] args)
		{
			return specification.AndIsValidRegex(selector, regex, false, message, args);
		}

		public static SpecificationEntity<TEntity> AndIsValidRegex<TEntity>(this SpecificationEntity<TEntity> specification, Func<TEntity, string> selector, string regex, bool stopIfInvalid, string message, params object[] args)
		{
			specification.Add(new AndIsValidRegex<TEntity>(selector, regex, message, args), stopIfInvalid);
			return specification;
		}
	}
}
