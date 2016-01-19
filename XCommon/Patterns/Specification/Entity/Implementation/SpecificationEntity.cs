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
        
        #region Custom
        public SpecificationEntity<TEntity> AndIsEmail(Expression<Func<TEntity, string>> selector)
        {
            return AndIsEmail(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsEmail(Expression<Func<TEntity, string>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.Email, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsCPF(Expression<Func<TEntity, string>> selector)
        {
            return AndIsCPF(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsCPF(Expression<Func<TEntity, string>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.CPF, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsCNPJ(Expression<Func<TEntity, string>> selector)
        {
            return AndIsCNPJ(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsCNPJ(Expression<Func<TEntity, string>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.CNPJ, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsURL(Expression<Func<TEntity, string>> selector)
        {
            return AndIsURL(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsURL(Expression<Func<TEntity, string>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.URL, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsPhone(Expression<Func<TEntity, string>> selector)
        {
            return AndIsPhone(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsPhone(Expression<Func<TEntity, string>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsValidRegex<TEntity>(selector, LibraryRegex.Phone, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsValidRegex(Expression<Func<TEntity, string>> selector, string regex)
        {
            Specifications.Add(new AndIsValidRegex<TEntity>(selector, regex, null, null));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsValidRegex(Expression<Func<TEntity, string>> selector, string regex, string message, params object[] args)
        {
            Specifications.Add(new AndIsValidRegex<TEntity>(selector, regex, message, args));
            return this;
        }

        #endregion

        #region AndIsNotEmpty
        public SpecificationEntity<TEntity> AndIsNotEmpty(Expression<Func<TEntity, string>> selector)
        {
            return AndIsNotEmpty(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsNotEmpty(Expression<Func<TEntity, string>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsNotEmpty<TEntity, string>(selector, AndIsNotEmptyType.String, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsNotEmpty(Expression<Func<TEntity, int?>> selector)
        {
            return AndIsNotEmpty(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsNotEmpty(Expression<Func<TEntity, int?>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsNotEmpty<TEntity, int?>(selector, AndIsNotEmptyType.Int, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsNotEmpty(Expression<Func<TEntity, decimal?>> selector)
        {
            return AndIsNotEmpty(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsNotEmpty(Expression<Func<TEntity, decimal?>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsNotEmpty<TEntity, decimal?>(selector, AndIsNotEmptyType.Decimal, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsNotEmpty(Expression<Func<TEntity, DateTime?>> selector)
        {
            return AndIsNotEmpty(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsNotEmpty(Expression<Func<TEntity, DateTime?>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsNotEmpty<TEntity, DateTime?>(selector, AndIsNotEmptyType.Date, message, args));
            return this;
        }       

        public SpecificationEntity<TEntity> AndIsNotEmpty<TValue>(Expression<Func<TEntity, TValue>> selector)
        {
            return AndIsNotEmpty(selector, null, null);
        }

        public SpecificationEntity<TEntity> AndIsNotEmpty<TValue>(Expression<Func<TEntity, TValue>> selector, string message, params object[] args)
        {
            Specifications.Add(new AndIsNotEmpty<TEntity, TValue>(selector, AndIsNotEmptyType.Object, message, args));
            return this;
        }
        #endregion

        #region AndCheckValue
        public SpecificationEntity<TEntity> AndIsBiggerThan(Expression<Func<TEntity, int>> value, Expression<Func<TEntity, int>> compareTo)
        {
            return AndIsBiggerThan(value, compareTo, null, null);
        }

        public SpecificationEntity<TEntity> AndIsBiggerThan(Expression<Func<TEntity, int>> value, Expression<Func<TEntity, int>> compareTo, string message, params object[] args)
        {
            Specifications.Add(new AndCheckValue<TEntity, int>(value, compareTo, null, AndCheckValueType.Int, AndCheckCompareType.BiggerThan, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsBiggerThan(Expression<Func<TEntity, decimal>> value, Expression<Func<TEntity, decimal>> compareTo)
        {
            return AndIsBiggerThan(value, compareTo, null, null);
        }

        public SpecificationEntity<TEntity> AndIsBiggerThan(Expression<Func<TEntity, decimal>> value, Expression<Func<TEntity, decimal>> compareTo, string message, params object[] args)
        {
            Specifications.Add(new AndCheckValue<TEntity, decimal>(value, compareTo, null, AndCheckValueType.Decimal, AndCheckCompareType.BiggerThan, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsBiggerThan(Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> compareTo)
        {
            return AndIsBiggerThan(value, compareTo, null, null);
        }

        public SpecificationEntity<TEntity> AndIsBiggerThan(Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> compareTo, string message, params object[] args)
        {
            Specifications.Add(new AndCheckValue<TEntity, DateTime>(value, compareTo, null, AndCheckValueType.Date, AndCheckCompareType.BiggerThan, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsBiggerThan(Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> compareTo, bool removeTime)
        {
            return AndIsBiggerThan(value, compareTo, removeTime, null, null);
        }

        public SpecificationEntity<TEntity> AndIsBiggerThan(Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> compareTo, bool removeTime, string message, params object[] args)
        {
            Specifications.Add(new AndCheckValue<TEntity, DateTime>(value, compareTo, null, AndCheckValueType.Date, AndCheckCompareType.BiggerThan, removeTime, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsLessThan(Expression<Func<TEntity, int>> value, Expression<Func<TEntity, int>> compareTo)
        {
            return AndIsLessThan(value, compareTo, null, null);
        }

        public SpecificationEntity<TEntity> AndIsLessThan(Expression<Func<TEntity, int>> value, Expression<Func<TEntity, int>> compareTo, string message, params object[] args)
        {
            Specifications.Add(new AndCheckValue<TEntity, int>(value, compareTo, null, AndCheckValueType.Int, AndCheckCompareType.LessThan, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsLessThan(Expression<Func<TEntity, decimal>> value, Expression<Func<TEntity, decimal>> compareTo)
        {
            return AndIsLessThan(value, compareTo, null, null);
        }

        public SpecificationEntity<TEntity> AndIsLessThan(Expression<Func<TEntity, decimal>> value, Expression<Func<TEntity, decimal>> compareTo, string message, params object[] args)
        {
            Specifications.Add(new AndCheckValue<TEntity, decimal>(value, compareTo, null, AndCheckValueType.Decimal, AndCheckCompareType.LessThan, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsLessThan(Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> compareTo)
        {
            return AndIsLessThan(value, compareTo, null, null);
        }

        public SpecificationEntity<TEntity> AndIsLessThan(Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> compareTo, string message, params object[] args)
        {
            Specifications.Add(new AndCheckValue<TEntity, DateTime>(value, compareTo, null, AndCheckValueType.Date, AndCheckCompareType.LessThan, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsLessThan(Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> compareTo, bool removeTime)
        {
            return AndIsLessThan(value, compareTo, removeTime, null, null);
        }

        public SpecificationEntity<TEntity> AndIsLessThan(Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> compareTo, bool removeTime, string message, params object[] args)
        {
            Specifications.Add(new AndCheckValue<TEntity, DateTime>(value, compareTo, null, AndCheckValueType.Date, AndCheckCompareType.LessThan, removeTime, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsInRange(Expression<Func<TEntity, int>> value, Expression<Func<TEntity, int>> start, Expression<Func<TEntity, int>> end)
        {
            return AndIsInRange(value, start, end, null, null);
        }

        public SpecificationEntity<TEntity> AndIsInRange(Expression<Func<TEntity, int>> value, Expression<Func<TEntity, int>> start, Expression<Func<TEntity, int>> end, string message, params object[] args)
        {
            Specifications.Add(new AndCheckValue<TEntity, int>(value, start, end, AndCheckValueType.Int, AndCheckCompareType.InRange, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsInRange(Expression<Func<TEntity, decimal>> value, Expression<Func<TEntity, decimal>> start, Expression<Func<TEntity, decimal>> end)
        {
            return AndIsInRange(value, start, end, null, null);
        }

        public SpecificationEntity<TEntity> AndIsInRange(Expression<Func<TEntity, decimal>> value, Expression<Func<TEntity, decimal>> start, Expression<Func<TEntity, decimal>> end, string message, params object[] args)
        {
            Specifications.Add(new AndCheckValue<TEntity, decimal>(value, start, end, AndCheckValueType.Decimal, AndCheckCompareType.InRange, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsInRange(Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> start, Expression<Func<TEntity, DateTime>> end)
        {
            return AndIsInRange(value, start, end, null, null);
        }

        public SpecificationEntity<TEntity> AndIsInRange(Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> start, Expression<Func<TEntity, DateTime>> end, string message, params object[] args)
        {
            Specifications.Add(new AndCheckValue<TEntity, DateTime>(value, start, end, AndCheckValueType.Date, AndCheckCompareType.InRange, message, args));
            return this;
        }

        public SpecificationEntity<TEntity> AndIsInRange(Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> start, Expression<Func<TEntity, DateTime>> end, bool removeTime)
        {
            return AndIsInRange(value, start, end, removeTime, null, null);
        }

        public SpecificationEntity<TEntity> AndIsInRange(Expression<Func<TEntity, DateTime>> value, Expression<Func<TEntity, DateTime>> start, Expression<Func<TEntity, DateTime>> end, bool removeTime, string message, params object[] args)
        {
            Specifications.Add(new AndCheckValue<TEntity, DateTime>(value, start, end, AndCheckValueType.Date, AndCheckCompareType.InRange, removeTime, message, args));
            return this;
        }
        #endregion

        #region Others
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
