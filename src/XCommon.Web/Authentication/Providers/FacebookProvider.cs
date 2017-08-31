using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.DependencyInjection;
using XCommon.Application.Authentication.Entity;

namespace XCommon.Web.Authentication.Providers
{
	public class FacebookProvider : BaseProvider
	{
		public FacebookProvider() : base(ProviderType.Facebook)
		{
		}

		public string AppId { get; internal set; }

		public string AppSecret { get; internal set; }

		public override SignUpExternalEntity ParseExternalEntity(OAuthCreatingTicketContext ctx)
		{
			throw new NotImplementedException();
		}

		public override void SetUp(AuthenticationBuilder authentication)
		{
			authentication.AddFacebook(c =>
			{
				c.AppId = AppId;
				c.AppSecret = AppSecret;
				c.SaveTokens = true;

				c.Scope.Add("email");
				c.Fields.Add("name");
				c.Fields.Add("email");

				c.Events = new OAuthEvents
				{
					OnRemoteFailure = ctx =>
					{
						return ProcessFail(ctx);
					},
					OnCreatingTicket = ctx =>
					{
						return ProcessTicket(ctx);
					}
				};
			});
		}
	}
}
