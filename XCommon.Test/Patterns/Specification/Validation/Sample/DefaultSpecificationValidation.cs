using XCommon.Patterns.Repository.Executes;
using XCommon.Patterns.Specification.Validation;

namespace XCommon.Test.Patterns.Specification.Validation.Sample
{
    public class DefaultSpecificationValidation : SpecificationValidation<PersonEntity>
    {
        public override bool IsSatisfiedBy(PersonEntity entity, Execute execute)
        {
            return CheckSpecifications(entity, execute);
        }
    }
}
