using System;
using XCommon.Application.Authentication.Entity;

namespace XCommon.EF.Application.Context.Register
{
	public class UsersProviders
	{
		public Guid IdUserProvide { get; set; }

		public Guid IdUser { get; set; }

		public ProviderType Provider { get; set; }

		public bool ProviderDefault { get; set; }

		public string ProviderToken { get; set; }

		public string ProviderUrlImage { get; set; }

		public string ProviderUrlCover { get; set; }

		public virtual Users Users { get; set; }
	}
}
