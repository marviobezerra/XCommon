using System;
using XCommon.Patterns.Repository.Executes;
using XCommon.Patterns.Specification.Validation;
using XCommon.Patterns.Specification.Validation.Extensions;
using XCommon.Test.Entity;

namespace XCommon.Test.Patterns.Specification.Validation.Sample
{
    public class CompleteSpecificationValidation : SpecificationValidation<PersonEntity>
    {
        public override bool IsSatisfiedBy(PersonEntity entity, Execute execute)
        {
            Specifications
                .AndIsValid(c => c.Id != Guid.Empty, "Invalid ID")
                .AndIsNotEmpty(c => c.Name, "Name is empty")
                .AndIsNotEmpty(c => c.Email, "Email is required")
                .AndIsEmail(c => c.Email, "Invalid email")
                .AndIsValid(c => c.Age > 0, "Age need's to be more than zero");


            return CheckSpecifications(entity, execute);
        }
    }
}
