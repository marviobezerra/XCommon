using System;
using XCommon.Application.Executes;

namespace XCommon.Patterns.Specification.Validation.Implementation
{
    internal class Or<TEntity> : ISpecificationValidation<TEntity>
    {
        private ISpecificationValidation<TEntity> Spec1 { get; set; }

        private ISpecificationValidation<TEntity> Spec2 { get; set; }

        private Func<TEntity, bool> Condition { get; set; }

        internal Or(ISpecificationValidation<TEntity> spec1, ISpecificationValidation<TEntity> spec2, bool condition)
            : this(spec1, spec2, c => condition)
        {
        }

        internal Or(ISpecificationValidation<TEntity> spec1, ISpecificationValidation<TEntity> spec2, Func<TEntity, bool> condition)
        {
            Spec1 = spec1;
            Spec2 = spec2;
            Condition = condition;
        }

        public bool IsSatisfiedBy(TEntity entity)
            => IsSatisfiedBy(entity, null);

        public bool IsSatisfiedBy(TEntity entity, Execute execute)
        {
            bool result = true;

            if (!Condition(entity))
                return result;

            Execute executeInternal1 = new Execute();
            Execute executeInternal2 = new Execute();

            bool result1 = Spec1.IsSatisfiedBy(entity, executeInternal1);
            bool result2 = Spec2.IsSatisfiedBy(entity, executeInternal2);
            result = result1 || result2;

            if (!result && execute != null)
            {
                execute.AddMessage(executeInternal1);
                execute.AddMessage(executeInternal2);
            }

            return result;
        }
    }
}
