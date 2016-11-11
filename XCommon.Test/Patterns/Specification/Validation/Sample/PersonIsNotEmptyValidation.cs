using XCommon.Patterns.Repository.Executes;
using XCommon.Patterns.Specification.Validation;
using XCommon.Patterns.Specification.Validation.Extensions;

namespace XCommon.Test.Patterns.Specification.Validation.Sample
{
    public class PersonIsNotEmptyValidation : SpecificationValidation<PersonEntity>
    {
        public override bool IsSatisfiedBy(PersonEntity entity, Execute execute)
        {
            Specifications
                .AndIsNotEmpty(c => c.Name)
                .AndIsNotEmpty(c => c.Email, "E-mail can't be empty");

            return CheckSpecifications(entity, execute);
        }
    }
}
