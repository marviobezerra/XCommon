using XCommon.Application.Executes;
using XCommon.Patterns.Specification.Validation;
using XCommon.Test.Entity;

namespace XCommon.Test.Patterns.Specification.Validation.Sample
{
    public class DefaultSpecificationValidation : SpecificationValidation<PersonEntity>
    {
        public override bool IsSatisfiedBy(PersonEntity entity, Execute execute)
        {
            var specifications = NewSpecificationList();

            return CheckSpecifications(specifications, entity, execute);
        }
    }
}
