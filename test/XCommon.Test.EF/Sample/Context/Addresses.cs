using System;

namespace XCommon.Test.EF.Sample.Context
{
	public class Addresses
    {
		public Guid IdAddress { get; set; }

		public Guid IdPerson { get; set; }

		public string PostalCode { get; set; }

		public string StreetName { get; set; }

		public string StreetNumber { get; set; }

		public virtual People People { get; set; }
	}
}
