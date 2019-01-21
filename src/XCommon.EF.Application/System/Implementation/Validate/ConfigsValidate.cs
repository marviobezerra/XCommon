using System;
using System.Threading.Tasks;
using XCommon.Application.Executes;
using XCommon.Entity.System;
using XCommon.Patterns.Specification.Validation;
using XCommon.Patterns.Specification.Validation.Extensions;

namespace XCommon.EF.Application.System.Implementation.Validate
{
	public class ConfigsValidate : SpecificationValidation<ConfigsEntity>
	{
		public override async Task<bool> IsSatisfiedByAsync(ConfigsEntity entity, Execute execute)
		{
			var spefications = NewSpecificationList()
				.AndIsValid(e => e.Key != Guid.Empty, Resources.Messages.DefaultKeyInvalid)
				.AndIsNotEmpty(e => e.ConfigKey, Resources.Messages.Config_KeyRequired)
				.AndIsNotEmpty(e => e.Module, Resources.Messages.Config_SectionRequired);

			return await CheckSpecificationsAsync(spefications, entity, execute);
		}
	}
}
