using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using XCommon.Web.Authentication.Base;

namespace XCommon.Web.Authentication.Facebook
{
    internal class AuthenticationFacebook : AuthenticationBase<AuthenticationFacebookConfig>
    {
        public AuthenticationFacebook(AuthenticationFacebookConfig config) 
            : base(config)
        {
        }

        internal override void Register(IApplicationBuilder app)
        {
            if (Config == null)
			{
				return;
			}

			FacebookOptions result = new FacebookOptions
            {
                AppId = Config.AppId,
                AppSecret = Config.AppSecret,
                Scope = { "email" },
                Fields = { "name", "email" },
                SaveTokens = true,
                SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme,
                Events = new OAuthEvents
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
                }
            };

            app
                .UseFacebookAuthentication(result);
        }
    }
}
