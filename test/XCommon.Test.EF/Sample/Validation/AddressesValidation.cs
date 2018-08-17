using System;
using System.Threading.Tasks;
using XCommon.Application.Executes;
using XCommon.Patterns.Specification.Validation;
using XCommon.Patterns.Specification.Validation.Extensions;
using XCommon.Test.EF.Sample.Entity;

namespace XCommon.Test.EF.Sample.Validation
{
	public class AddressesValidation : SpecificationValidation<AddressesEntity>
	{
		public override async Task<bool> IsSatisfiedByAsync(AddressesEntity entity, Execute execute)
		{
			var specification = NewSpecificationList()
			   .AndIsValid(c => c.Key != Guid.Empty, "Invalid ID")
			   .AndIsNotEmpty(c => c.StreetName, "Street Name is empty")
			   .AndIsNotEmpty(c => c.PostalCode, "Postal code is required");

			return await CheckSpecificationsAsync(specification, entity, execute);
		}
	}
}
