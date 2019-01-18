using System;
using System.Runtime.Serialization;
using XCommon.Application.Authentication.Entity;
using XCommon.Patterns.Repository.Entity;

namespace XCommon.Entity.Register
{
	public partial class UsersProvidersEntity : EntityBase
	{
		public Guid IdUserProvide { get; set; }

		public Guid IdUser { get; set; }

		public ProviderType Provider { get; set; }

		public bool ProviderDefault { get; set; }

		public string ProviderToken { get; set; }

		public string ProviderUrlImage { get; set; }

		public string ProviderUrlCover { get; set; }


		[IgnoreDataMember]
		public override Guid Key
		{
			get
			{
				return IdUserProvide;
			}
			set
			{
				IdUserProvide = value;
			}
		}
	}
}
