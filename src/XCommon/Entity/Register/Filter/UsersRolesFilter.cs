using System;
using System.Collections.Generic;
using XCommon.Patterns.Repository.Entity;

namespace XCommon.Entity.Register.Filter
{
	public class UsersRolesFilter : FilterBase
	{
		public UsersRolesFilter()
		{
			IdUsers = new List<Guid>();
		}

		public Guid? IdUser { get; set; }

		public List<Guid> IdUsers { get; set; }
	}
}
