using System;
using System.Runtime.Serialization;
using XCommon.Patterns.Repository.Entity;

namespace XCommon.Entity.Register
{
	public class UsersEntity : EntityBase
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


		[IgnoreDataMember]
		public override Guid Key
		{
			get
			{
				return IdUser;
			}
			set
			{
				IdUser = value;
			}
		}
	}
}
