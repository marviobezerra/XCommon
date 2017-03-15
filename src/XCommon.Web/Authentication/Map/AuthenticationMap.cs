using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using XCommon.Web.Authentication.Base;

namespace XCommon.Web.Authentication.Map
{
    internal class AuthenticationMap : AuthenticationBase<AuthenticationMapConfig>
    {
        public AuthenticationMap(AuthenticationMapConfig config) 
            : base(config)
        {
        }

        internal override void Register(IApplicationBuilder app)
        {
            if (Config == null)
			{
				return;
			}

			app
               .Map(Config.UriLogin, signoutApp =>
               {
                   signoutApp.Run(async context =>
                   {
                       var authType = context.Request.Query[Config.UriParam];
                       if (!string.IsNullOrEmpty(authType))
                       {
                            // By default the client will be redirect back to the URL that issued the challenge (/login?authtype=foo),
                            // send them to the home page instead (/).
                            await context.Authentication.ChallengeAsync(authType, new AuthenticationProperties() { RedirectUri = Config.UriRedirect });
                           return;
                       }

                       context.Response.ContentType = "text/html";
                       await context.Response.WriteAsync("<html><body>");
                       await context.Response.WriteAsync("Choose an authentication scheme: <br>");
                       foreach (var type in context.Authentication.GetAuthenticationSchemes())
                       {
                           await context.Response.WriteAsync("<a href=\"?authscheme=" + type.AuthenticationScheme + "\">" + (type.DisplayName ?? "(suppressed)") + "</a><br>");
                       }
                       await context.Response.WriteAsync("</body></html>");
                   });
               });
        }
    }
}
