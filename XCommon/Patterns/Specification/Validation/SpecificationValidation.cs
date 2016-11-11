using XCommon.Patterns.Repository.Executes;
using XCommon.Patterns.Specification.Validation.Implementation;

namespace XCommon.Patterns.Specification.Validation
{
    public abstract class SpecificationValidation<TEntity> : ISpecificationValidation<TEntity>
    {
        public SpecificationValidation()
        {
            Specifications = new SpecificationList<TEntity>();

            ISpecificationValidation<TEntity> basicSpecification = new AndIsNotEmpty<TEntity, object>(c => c, AndIsNotEmptyType.Object, true, "Entity {0} can't be null", typeof(TEntity).Name);
            Specifications.Add(basicSpecification);
        }

        protected SpecificationList<TEntity> Specifications { get; set; }

        public bool IsSatisfiedBy(TEntity entity)
        {
            return IsSatisfiedBy(entity, new Execute());
        }

        public abstract bool IsSatisfiedBy(TEntity entity, Execute execute);

        protected virtual bool CheckSpecifications(TEntity entity, Execute execute)
        {
            bool result = true;

            foreach (var item in Specifications.Items)
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
