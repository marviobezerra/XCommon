using XCommon.Patterns.Specification.Entity.Implementation;
using XCommon.Util;
using System;
using System.Linq.Expressions;

namespace XCommon.Patterns.Specification.Entity
{
    public static class SpecificationEntityExtensions
    {
        public static ISpecificationEntity<TEntity> And<TEntity>(this ISpecificationEntity<TEntity> spec1, ISpecificationEntity<TEntity> spec2)
        {
            return new AndSpecification<TEntity>(spec1, spec2);
        }

        public static ISpecificationEntity<TEntity> AndIsEmail<TEntity, TValue>(this ISpecificationEntity<TEntity> spec1, Expression<Func<TEntity, TValue>> selector, string message, params object[] args)
        {
            var memberSelector = (MemberExpression)selector.Body;
            var property = (memberSelector as MemberExpression).Member.Name;

            return new AndIsValidRegex<TEntity>(spec1, property, LibraryRegex.Email, message, args);
        }

        public static ISpecificationEntity<TEntity> AndIsCPF<TEntity, TValue>(this ISpecificationEntity<TEntity> spec1, Expression<Func<TEntity, TValue>> selector, string message, params object[] args)
        {
            var memberSelector = (MemberExpression)selector.Body;
            var property = (memberSelector as MemberExpression).Member.Name;

            return new AndIsValidRegex<TEntity>(spec1, property, LibraryRegex.CPF, message, args);
        }

        public static ISpecificationEntity<TEntity> AndIsCNPJ<TEntity, TValue>(this ISpecificationEntity<TEntity> spec1, Expression<Func<TEntity, TValue>> selector, string message, params object[] args)
        {
            var memberSelector = (MemberExpression)selector.Body;
            var property = (memberSelector as MemberExpression).Member.Name;

            return new AndIsValidRegex<TEntity>(spec1, property, LibraryRegex.CNPJ, message, args);
        }

        public static ISpecificationEntity<TEntity> AndIsURL<TEntity, TValue>(this ISpecificationEntity<TEntity> spec1, Expression<Func<TEntity, TValue>> selector, string message, params object[] args)
        {
            var memberSelector = (MemberExpression)selector.Body;
            var property = (memberSelector as MemberExpression).Member.Name;

            return new AndIsValidRegex<TEntity>(spec1, property, LibraryRegex.URL, message, args);
        }

        public static ISpecificationEntity<TEntity> AndIsPhone<TEntity, TValue>(this ISpecificationEntity<TEntity> spec1, Expression<Func<TEntity, TValue>> selector, string message, params object[] args)
        {
            var memberSelector = (MemberExpression)selector.Body;
            var property = (memberSelector as MemberExpression).Member.Name;

            return new AndIsValidRegex<TEntity>(spec1, property, LibraryRegex.Phone, message, args);
        }

        public static ISpecificationEntity<TEntity> AndIsValid<TEntity>(this ISpecificationEntity<TEntity> spec1, Func<TEntity, bool> selector, string message, params object[] args)
        {
            return new AndIsValid<TEntity>(spec1, selector, message, args);
        }

        public static ISpecificationEntity<TEntity> Or<TEntity>(this ISpecificationEntity<TEntity> spec1, ISpecificationEntity<TEntity> spec2)
        {
            return new OrSpecification<TEntity>(spec1, spec2);
        }

        public static ISpecificationEntity<TEntity> Not<TEntity>(this ISpecificationEntity<TEntity> spec1, ISpecificationEntity<TEntity> spec2)
        {
            return new NotSpecification<TEntity>(spec1, spec2);
        }
    }
}
