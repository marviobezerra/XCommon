using System;
using System.Runtime.Serialization;
using XCommon.Patterns.Repository.Entity;

namespace XCommon.Entity.Register
{
	public class UsersRolesEntity : EntityBase
	{
		public Guid IdUserRole { get; set; }

		public Guid IdUser { get; set; }

		public string Role { get; set; }

		[IgnoreDataMember]
		public override Guid Key
		{
			get
			{
				return IdUserRole;
			}
			set
			{
				IdUserRole = value;
			}
		}
	}
}
