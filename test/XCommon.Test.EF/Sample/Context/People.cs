using System;
using System.Collections.Generic;

namespace XCommon.Test.EF.Sample.Context
{
	public class People
    {
		public Guid IdPerson { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }

		public virtual ICollection<Addresses> Addresses { get; set; }
	}
}
