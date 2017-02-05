using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using XCommon.Web.Authentication.Base;

namespace XCommon.Web.Authentication.Cookie
{
    internal class AuthenticationCookie : AuthenticationBase<AuthenticationCookieConfig>
    {
        public AuthenticationCookie(AuthenticationCookieConfig config) 
            : base(config)
        {
        }

        internal override void Register(IApplicationBuilder app)
        {
            if (Config == null)
                return;

            CookieAuthenticationOptions result = new CookieAuthenticationOptions
            {
                AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme,
                AutomaticAuthenticate = true,
                CookieName = Config.CookieName
            };

            app
                .UseCookieAuthentication(result);
        }
    }
}
