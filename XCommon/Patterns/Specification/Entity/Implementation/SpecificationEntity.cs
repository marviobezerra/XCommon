using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using XCommon.Patterns.Repository.Executes;
using XCommon.Util;

namespace XCommon.Patterns.Specification.Entity.Implementation
{
    public class SpecificationEntity<TEntity> : ISpecificationEntity<TEntity>
    {
        public SpecificationEntity()
        {
            Specifications = new List<ISpecificationEntity<TEntity>>();
        }

        private List<ISpecificationEntity<TEntity>> Specifications { get; set; }

        public bool IsSatisfiedBy(TEntity entity)
        {
            return IsSatisfiedBy(entity, new Execute());
        }

        public bool IsSatisfiedBy(TEntity entity, Execute execute)
        {
            bool result = true;

            foreach (var item in Specifications)
            {
                var satisfied = item.IsSatisfiedBy(entity, execute);
                result = result && satisfied;
            }

            return result;
        }

        #region Implentations
        public SpecificationEntity<TEntity> AndIsEmail<TValue>(Expression<Func<TEntity, TValue>> selector)
        {
            return AndIsEmail(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsEmail<TValue>(Expression<Func<TEntity, TValue>> selector, string message, params object[] args)
        {
            var memberSelector = (MemberExpression)selector.Body;
            var property = (memberSelector as MemberExpression).Member.Name;
            Specifications.Add(new AndIsValidRegex<TEntity>(property, LibraryRegex.Email, message, args));

            return this;
        }

        public SpecificationEntity<TEntity> AndIsCPF<TValue>(Expression<Func<TEntity, TValue>> selector)
        {
            return AndIsCPF(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsCPF<TValue>(Expression<Func<TEntity, TValue>> selector, string message, params object[] args)
        {
            var memberSelector = (MemberExpression)selector.Body;
            var property = (memberSelector as MemberExpression).Member.Name;

            Specifications.Add(new AndIsValidRegex<TEntity>(property, LibraryRegex.CPF, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsCNPJ<TValue>(Expression<Func<TEntity, TValue>> selector)
        {
            return AndIsCNPJ(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsCNPJ<TValue>(Expression<Func<TEntity, TValue>> selector, string message, params object[] args)
        {
            var memberSelector = (MemberExpression)selector.Body;
            var property = (memberSelector as MemberExpression).Member.Name;

            Specifications.Add(new AndIsValidRegex<TEntity>(property, LibraryRegex.CNPJ, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsURL<TValue>(Expression<Func<TEntity, TValue>> selector)
        {
            return AndIsURL(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsURL<TValue>(Expression<Func<TEntity, TValue>> selector, string message, params object[] args)
        {
            var memberSelector = (MemberExpression)selector.Body;
            var property = (memberSelector as MemberExpression).Member.Name;

            Specifications.Add(new AndIsValidRegex<TEntity>(property, LibraryRegex.URL, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsPhone<TValue>(Expression<Func<TEntity, TValue>> selector)
        {
            return AndIsPhone(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsPhone<TValue>(Expression<Func<TEntity, TValue>> selector, string message, params object[] args)
        {
            var memberSelector = (MemberExpression)selector.Body;
            var property = (memberSelector as MemberExpression).Member.Name;

            Specifications.Add(new AndIsValidRegex<TEntity>(property, LibraryRegex.Phone, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsValid(Func<TEntity, bool> selector)
        {
            return AndIsValid(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsValid(Func<TEntity, bool> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsValid<TEntity>(selector, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> And(ISpecificationEntity<TEntity> specification)
        {
            Specifications.Add(specification);
            return this;
        }

        public SpecificationEntity<TEntity> Or(ISpecificationEntity<TEntity> spec1, ISpecificationEntity<TEntity> spec2)
        {
            Specifications.Add(new OrSpecification<TEntity>(spec1, spec2));
            return this;
        }
        #endregion
    }
}
