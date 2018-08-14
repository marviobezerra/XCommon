using System;
using System.Threading.Tasks;
using XCommon.Application.Executes;
using XCommon.Patterns.Specification.Validation;
using XCommon.Patterns.Specification.Validation.Extensions;
using XCommon.Test.EF.Sample.Entity;

namespace XCommon.Test.EF.Sample.Validation
{
	public class PeopleValidation : SpecificationValidation<PeopleEntity>
	{
		public override async Task<bool> IsSatisfiedByAsync(PeopleEntity entity, Execute execute)
		{
			var specification = NewSpecificationList()
			   .AndIsValid(c => c.Key != Guid.Empty, "Invalid ID")
			   .AndIsNotEmpty(c => c.Name, "Name is empty")
			   .AndIsNotEmpty(c => c.Email, "Email is required")
			   .AndIsEmail(c => c.Email, "Invalid email");

			return await CheckSpecificationsAsync(specification, entity, execute);
		}
	}
}
