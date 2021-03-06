using System;
using System.Threading.Tasks;
using XCommon.Application.Executes;
using XCommon.Patterns.Specification.Validation;
using XCommon.Patterns.Specification.Validation.Extensions;
using XCommon.Test.Entity;

namespace XCommon.Test.Patterns.Specification.Validation.Sample
{
    public class CompleteSpecificationValidation : SpecificationValidation<PersonEntity>
    {
        public override async Task<bool> IsSatisfiedByAsync(PersonEntity entity, Execute execute)
        {
            var specification = NewSpecificationList()
                .AndIsValid(c => c.Id != Guid.Empty, "Invalid ID")
                .AndIsNotEmpty(c => c.Name, "Name is empty")
                .AndIsNotEmpty(c => c.Email, "Email is required")
                .AndIsEmail(c => c.Email, "Invalid email")
                .AndIsValid(c => c.Age > 0, "Age need's to be more than zero");


            return await CheckSpecificationsAsync(specification, entity, execute);
        }
    }
}
