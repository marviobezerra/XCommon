using System;
using System.Collections.Generic;
using XCommon.Patterns.Repository.Entity;

namespace XCommon.Entity.Register.Filter
{
	public class UsersTokensFilter : FilterBase
	{
		public UsersTokensFilter()
		{
			IdUsers = new List<Guid>();
		}

		public Guid? IdUser { get; set; }

		public string TokenType { get; set; }

		public List<Guid> IdUsers { get; set; }
	}
}
