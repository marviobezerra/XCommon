using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.DependencyInjection;
using XCommon.Application.Authentication.Entity;

namespace XCommon.Web.Authentication2.Providers
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
				c.Scope.Add("email");
				c.Fields.Add("name");
				c.Fields.Add("email");
				c.SaveTokens = true;
				c.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				c.Events = new OAuthEvents
				{
					OnRemoteFailure = ctx =>
					{
						ctx.Response.Redirect("/error?FailureMessage=" + UrlEncoder.Default.Encode(ctx.Failure.Message));
						ctx.HandleResponse();
						return Task.FromResult(0);
					},
					OnCreatingTicket = ctx =>
					{
						return Task.FromResult(0);
					},
					OnTicketReceived = ctx =>
					{
						return Task.FromResult(0);
					}
				};
			});
		}
	}
}
