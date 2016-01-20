using System;
using System.Linq.Expressions;
using XCommon.Patterns.Specification.Entity.Implementation;
using XCommon.Util;

namespace XCommon.Patterns.Specification.Entity.Extensions
{
    public static class SpecificationCustom
    {
        public static SpecificationEntity<TEntity> AndIsEmail<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, string>> selector, bool stopIfError = false)
        {
            return specification.AndIsEmail(selector, stopIfError, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsEmail<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, string>> selector, string message, params object[] args)
        {
            return specification.AndIsEmail(selector, false, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsEmail<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, string>> selector, bool stopIfError, string message, params object[] args)
        {
            specification.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.Email, message, args), stopIfError);
            return specification;
        }

        public static SpecificationEntity<TEntity> AndIsCPF<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, string>> selector, bool stopIfError = false)
        {
            return specification.AndIsCPF(selector, stopIfError, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsCPF<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, string>> selector, string message, params object[] args)
        {
            return specification.AndIsCPF(selector, false, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsCPF<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, string>> selector, bool stopIfError, string message, params object[] args)
        {
            specification.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.CPF, message, args), stopIfError);
            return specification;
        }

        public static SpecificationEntity<TEntity> AndIsCNPJ<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, string>> selector, bool stopIfError = false)
        {
            return specification.AndIsCNPJ(selector, stopIfError, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsCNPJ<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, string>> selector, string message, params object[] args)
        {
            return specification.AndIsCNPJ(selector, false, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsCNPJ<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, string>> selector, bool stopIfError, string message, params object[] args)
        {
            specification.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.CNPJ, message, args), stopIfError);
            return specification;
        }

        public static SpecificationEntity<TEntity> AndIsURL<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, string>> selector, bool stopIfError = false)
        {
            return specification.AndIsURL(selector, stopIfError, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsURL<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, string>> selector, string message, params object[] args)
        {
            return specification.AndIsURL(selector, false, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsURL<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, string>> selector, bool stopIfError, string message, params object[] args)
        {
            specification.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.URL, message, args), stopIfError);
            return specification;
        }

        public static SpecificationEntity<TEntity> AndIsPhone<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, string>> selector, bool stopIfError = false)
        {
            return specification.AndIsPhone(selector, stopIfError, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsPhone<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, string>> selector, string message, params object[] args)
        {
            return specification.AndIsPhone(selector, false, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsPhone<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, string>> selector, bool stopIfError, string message, params object[] args)
        {
            specification.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.Phone, message, args), stopIfError);
            return specification;
        }

        public static SpecificationEntity<TEntity> AndIsValidRegex<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, string>> selector, string regex, bool stopIfError = false)
        {
            return specification.AndIsValidRegex(selector, regex, stopIfError, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsValidRegex<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, string>> selector, string regex, string message, params object[] args)
        {
            return specification.AndIsValidRegex(selector, regex, false, null, null);
        }

        public static SpecificationEntity<TEntity> AndIsValidRegex<TEntity>(this SpecificationEntity<TEntity> specification, Expression<Func<TEntity, string>> selector, string regex, bool stopIfError, string message, params object[] args)
        {
            specification.Add(new AndIsValidRegex<TEntity>(selector, regex, message, args), stopIfError);
            return specification;
        }
    }
}
