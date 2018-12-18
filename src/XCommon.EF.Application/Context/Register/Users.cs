using System;
using System.Collections.Generic;

namespace XCommon.EF.Application.Context.Register
{
	public class Users
	{
		public Guid IdUser { get; set; }

		public Guid IdPerson { get; set; }

		public int AccessFailedCount { get; set; }

		public bool LockoutEnabled { get; set; }

		public DateTime? LockoutEnd { get; set; }

		public bool EmailConfirmed { get; set; }

		public bool PhoneConfirmed { get; set; }

		public string PasswordHash { get; set; }

		public bool ProfileComplete { get; set; }

		public virtual People People { get; set; }

		public virtual ICollection<UsersProviders> UsersProviders { get; set; }
	}
}
