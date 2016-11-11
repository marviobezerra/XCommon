using System;
using XCommon.Patterns.Repository.Executes;
using XCommon.Patterns.Specification.Validation;
using XCommon.Patterns.Specification.Validation.Extensions;

namespace XCommon.Test.Patterns.Specification.Validation.Sample
{
    public class PersonIsValidValidation : SpecificationValidation<PersonEntity>
    {
        public override bool IsSatisfiedBy(PersonEntity entity, Execute execute)
        {
            Specifications
                .AndIsValid(c => c.Age >= 28)
                .AndIsValid(c => c.Id != Guid.Empty, "Invalid id");

            return CheckSpecifications(entity, execute);
        }
    }
}
