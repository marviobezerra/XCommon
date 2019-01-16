using System;

namespace XCommon.EF.Application.Context.Register
{
	public class UsersRoles
	{
		public Guid IdUserRole { get; set; }

		public Guid IdUser { get; set; }

		public string Role { get; set; }

		public virtual Users Users { get; set; }
	}
}
