using System.Threading.Tasks;
using XCommon.Application.Authentication.Entity;
using XCommon.Application.Executes;
using XCommon.Patterns.Specification.Validation;
using XCommon.Patterns.Specification.Validation.Extensions;

namespace XCommon.EF.Application.Authentication.Validation
{
	public class SignInValidate : SpecificationValidation<SignInEntity>
	{
		public override async Task<bool> IsSatisfiedByAsync(SignInEntity entity, Execute execute)
		{
			var spefications = NewSpecificationList()
				.AndIsEmail(e => e.User, Resources.Authentication.InvalidEmail)
				.AndIsNotEmpty(e => e.User, Resources.Authentication.RequiredUser)
				.AndIsNotEmpty(e => e.Password, Resources.Authentication.RequiredPassword);

			return await CheckSpecificationsAsync(spefications, entity, execute);
		}
	}
}
