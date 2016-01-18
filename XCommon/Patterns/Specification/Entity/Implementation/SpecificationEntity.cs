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

        #region Helper
        private string GetPropertyName<TValue>(Expression<Func<TEntity, TValue>> selector)
        {
            var memberSelector = (MemberExpression)selector.Body;
            var propertyName = (memberSelector as MemberExpression).Member.Name;
            return propertyName;
        }
        #endregion

        #region Implentations
        public SpecificationEntity<TEntity> AndIsEmail(Expression<Func<TEntity, string>> selector)
        {
            return AndIsEmail(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsEmail(Expression<Func<TEntity, string>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsValidRegex<TEntity>(GetPropertyName(selector), LibraryRegex.Email, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsCPF(Expression<Func<TEntity, string>> selector)
        {
            return AndIsCPF(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsCPF(Expression<Func<TEntity, string>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsValidRegex<TEntity>(GetPropertyName(selector), LibraryRegex.CPF, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsCNPJ(Expression<Func<TEntity, string>> selector)
        {
            return AndIsCNPJ(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsCNPJ(Expression<Func<TEntity, string>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsValidRegex<TEntity>(GetPropertyName(selector), LibraryRegex.CNPJ, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsURL(Expression<Func<TEntity, string>> selector)
        {
            return AndIsURL(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsURL(Expression<Func<TEntity, string>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsValidRegex<TEntity>(GetPropertyName(selector), LibraryRegex.URL, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsPhone(Expression<Func<TEntity, string>> selector)
        {
            return AndIsPhone(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsPhone(Expression<Func<TEntity, string>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsValidRegex<TEntity>(GetPropertyName(selector), LibraryRegex.Phone, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsNotEmpty(Expression<Func<TEntity, string>> selector)
        {
            return AndIsNotEmpty(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsNotEmpty(Expression<Func<TEntity, string>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsNotEmpty<TEntity>(GetPropertyName(selector), AndIsNotEmptyType.String, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsNotEmpty(Expression<Func<TEntity, int?>> selector)
        {
            return AndIsNotEmpty(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsNotEmpty(Expression<Func<TEntity, int?>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsNotEmpty<TEntity>(GetPropertyName(selector), AndIsNotEmptyType.Int, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsNotEmpty(Expression<Func<TEntity, decimal?>> selector)
        {
            return AndIsNotEmpty(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsNotEmpty(Expression<Func<TEntity, decimal?>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsNotEmpty<TEntity>(GetPropertyName(selector), AndIsNotEmptyType.Decimal, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsNotEmpty(Expression<Func<TEntity, DateTime?>> selector)
        {
            return AndIsNotEmpty(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsNotEmpty(Expression<Func<TEntity, DateTime?>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsNotEmpty<TEntity>(GetPropertyName(selector), AndIsNotEmptyType.Date, message, args));
            return this;
        }       

        public SpecificationEntity<TEntity> AndIsNotEmpty<TValue>(Expression<Func<TEntity, TValue>> selector)
        {
            return AndIsNotEmpty(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsNotEmpty<TValue>(Expression<Func<TEntity, TValue>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsNotEmpty<TEntity>(GetPropertyName(selector), AndIsNotEmptyType.Object, message, args));
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
