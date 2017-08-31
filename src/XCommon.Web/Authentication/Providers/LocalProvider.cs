using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using XCommon.Application.Authentication.Entity;

namespace XCommon.Web.Authentication.Providers
{
	public class LocalProvider : BaseProvider
	{
		public LocalProvider() : base(ProviderType.Local)
		{
		}

		public override SignUpExternalEntity ParseExternalEntity(OAuthCreatingTicketContext ctx)
		{
			throw new NotImplementedException();
		}

		public override void SetUp(AuthenticationBuilder authentication)
		{
			throw new NotImplementedException();
		}
	}
}
