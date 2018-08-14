using System;
using XCommon.Patterns.Repository.Entity;

namespace XCommon.Test.EF.Sample.Entity
{
	public class AddressesEntity : EntityBase
	{
		public Guid IdAddress { get; set; }

		public Guid IdPerson { get; set; }

		public string PostalCode { get; set; }

		public string StreetName { get; set; }

		public string StreetNumber { get; set; }
		public override Guid Key
		{
			get => IdAddress;
			set => IdAddress = value;
		}
	}
}
