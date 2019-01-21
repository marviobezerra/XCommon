using System;
using System.Threading.Tasks;
using XCommon.Application.Executes;
using XCommon.Entity.Register;
using XCommon.Patterns.Specification.Validation;
using XCommon.Patterns.Specification.Validation.Extensions;

namespace XCommon.EF.Application.Register.Implementation.Validate
{
	public class UsersRolesValidate : SpecificationValidation<UsersRolesEntity>
	{
		public override async Task<bool> IsSatisfiedByAsync(UsersRolesEntity entity, Execute execute)
		{
			var spefications = NewSpecificationList()
				.AndIsValid(e => e.Key != Guid.Empty, Resources.Messages.DefaultKeyInvalid);

			return await CheckSpecificationsAsync(spefications, entity, execute);
		}
	}
}