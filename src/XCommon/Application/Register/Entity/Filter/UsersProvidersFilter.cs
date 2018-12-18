using System;
using XCommon.Application.Authentication.Entity;
using XCommon.Patterns.Repository.Entity;

namespace XCommon.Application.Register.Entity.Filter
{
	public class UsersProvidersFilter : FilterBase
	{
		public Guid? IdUser { get; set; }

		public ProviderType? Provider { get; set; }
	}
}
