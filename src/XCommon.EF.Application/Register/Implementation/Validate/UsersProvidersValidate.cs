using System;
using System.Threading.Tasks;
using XCommon.Application.Executes;
using XCommon.Application.Register.Entity;
using XCommon.Patterns.Specification.Validation;
using XCommon.Patterns.Specification.Validation.Extensions;

namespace XCommon.EF.Application.Register.Implementation.Validate
{
	public class UsersProvidersValidate : SpecificationValidation<UsersProvidersEntity>
	{
		public override async Task<bool> IsSatisfiedByAsync(UsersProvidersEntity entity, Execute execute)
		{
			var spefications = NewSpecificationList()
				.AndIsValid(e => e.Key != Guid.Empty, "Default key isn't valid");

			return await CheckSpecificationsAsync(spefications, entity, execute);
		}
	}
}
