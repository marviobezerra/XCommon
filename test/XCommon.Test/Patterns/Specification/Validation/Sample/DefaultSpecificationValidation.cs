using System.Threading.Tasks;
using XCommon.Application.Executes;
using XCommon.Patterns.Specification.Validation;
using XCommon.Test.Entity;

namespace XCommon.Test.Patterns.Specification.Validation.Sample
{
    public class DefaultSpecificationValidation : SpecificationValidation<PersonEntity>
    {
        public override async Task<bool> IsSatisfiedByAsync(PersonEntity entity, Execute execute)
        {
            var specifications = NewSpecificationList();

            return await CheckSpecificationsAsync(specifications, entity, execute);
        }
    }
}
