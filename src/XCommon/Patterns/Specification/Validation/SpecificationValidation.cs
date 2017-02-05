using XCommon.Application.Executes;
using XCommon.Patterns.Specification.Validation.Implementation;

namespace XCommon.Patterns.Specification.Validation
{
    public abstract class SpecificationValidation<TEntity> : ISpecificationValidation<TEntity>
    {
        protected SpecificationList<TEntity> NewSpecificationList(bool addCheckNullObject = true)
        {
            SpecificationList<TEntity> result = new SpecificationList<TEntity>();

            if (addCheckNullObject)
            {
                ISpecificationValidation<TEntity> basicSpecification = new AndIsNotEmpty<TEntity, object>(c => c, AndIsNotEmptyType.Object, true, "Entity {0} can't be null", typeof(TEntity).Name);
                result.Add(basicSpecification, true);
            }
            
            return result;
        }

        public bool IsSatisfiedBy(TEntity entity)
        {
            return IsSatisfiedBy(entity, new Execute());
        }

        public abstract bool IsSatisfiedBy(TEntity entity, Execute execute);

        protected virtual bool CheckSpecifications(SpecificationList<TEntity> specifications, TEntity entity, Execute execute)
        {
            bool result = true;

            foreach (var item in specifications.Items)
            {
                if (!item.Condition(entity))
                    continue;

                var satisfied = item.Specification.IsSatisfiedBy(entity, execute);
                result = result && satisfied;

                if (!result && item.StopIfInvalid)
                    break;
            }

            return result;
        }
    }
}
