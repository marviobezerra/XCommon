using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.MicrosoftAccount;
using Microsoft.Owin.Security.OAuth;
using Owin;
using XCommon.Web.WebApi.Controller;
using System;
using System.Threading.Tasks;

namespace XCommon.Web.Owin
{
    public abstract class IdentityApp
    {
        private bool IsAjaxRequest(IOwinRequest request)
        {
            IReadableStringCollection query = request.Query;

            if ((query != null) && (query["X-Requested-With"] == "XMLHttpRequest"))
                return true;

            IHeaderDictionary headers = request.Headers;
            return ((headers != null) && (headers["X-Requested-With"] == "XMLHttpRequest"));
        }

        protected abstract FacebookAuthenticationOptions GetFacebookConfig();
        protected abstract GoogleOAuth2AuthenticationOptions GetGoogleConfig();
        protected abstract MicrosoftAccountAuthenticationOptions GetMicrosoftConfig();
        protected abstract OAuthAuthorizationServerOptions GetOAuthConfig();
        protected abstract string LoginUrl();

        protected OAuthAuthorizationServerOptions GetOAuthConfigDefault()
        {
            OAuthAuthorizationServerOptions result = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            return result;
        }

        public virtual void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString(LoginUrl()),
                Provider = new CookieAuthenticationProvider
                {
                    OnResponseSignIn = ctx => {
                        var teste = ctx.Options;
                    },
                    OnResponseSignedIn = ctx => {
                        var teste = ctx.Options;
                    },
                    OnValidateIdentity = ctx => {
                        
                        var result = Task.Run(() => { 
                        
                        });

                        return result;
                    },
                    OnApplyRedirect = ctx =>
                    {
                        if (!IsAjaxRequest(ctx.Request))
                            ctx.Response.Redirect(ctx.RedirectUri);
                    }                    
                }
            });
            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            var microsoftConfig = GetMicrosoftConfig();
            var facebookConfig = GetFacebookConfig();
            var googleConfig = GetGoogleConfig();
            var oAuthConfig = GetOAuthConfig();

            if (microsoftConfig != null)
            {
                microsoftConfig.Scope.Add("wl.basic");
                microsoftConfig.Scope.Add("wl.emails");
                app.UseMicrosoftAccountAuthentication(microsoftConfig);
            }

            if (facebookConfig != null)
            {
                facebookConfig.Scope.Add("email");
                app.UseFacebookAuthentication(facebookConfig);
            }

            if (googleConfig != null)
            {
                app.UseGoogleAuthentication(googleConfig);
            }

            if (oAuthConfig != null)
            {
                app.UseOAuthAuthorizationServer(oAuthConfig);
                app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            }
        }
    }
}
