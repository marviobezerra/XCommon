using System.Threading.Tasks;
using XCommon.Application.Authentication.Entity;
using XCommon.Application.Executes;
using XCommon.EF.Application.Register.Interface;
using XCommon.Entity.Register.Filter;
using XCommon.Patterns.Ioc;
using XCommon.Patterns.Specification.Validation;
using XCommon.Patterns.Specification.Validation.Extensions;
using XCommon.Patterns.Specification.Validation.Implementation;

namespace XCommon.EF.Application.Authentication.Validation
{
	public class SignUpValidate : SpecificationValidation<SignUpEntity>
	{
		private IPeopleBusiness PeopleBusiness => Kernel.Resolve<IPeopleBusiness>();

		public override async Task<bool> IsSatisfiedByAsync(SignUpEntity entity, Execute execute)
		{
			var spefications = NewSpecificationList()
				.AndIsNotEmpty(e => e.Name, Resources.Authentication.RequiredName)
				.AndIsNotEmpty(e => e.Email, Resources.Authentication.RequiredEmail)
				.AndIsValidAsync(e => CheckDuplicatedEmailAsync(e.Email), Resources.Authentication.EmailDuplicated)
				.AndMerge(ValidatePassword(), e => e.Provider == ProviderType.Local);

			return await CheckSpecificationsAsync(spefications, entity, execute);
		}

		private async Task<bool> CheckDuplicatedEmailAsync(string email)
		{
			var person = await PeopleBusiness.GetFirstByFilterAsync(new PeopleFilter { Email = email });
			return person == null;
		}

		private SpecificationList<SignUpEntity> ValidatePassword()
		{
			var spefications = NewSpecificationList()
				.AndIsNotEmpty(e => e.Password, Resources.Authentication.RequiredPassword)
				.AndIsValid(e => e.Password == e.PasswordConfirm, Resources.Authentication.PasswordNotMatch);

			return spefications;
		}
	}
}
