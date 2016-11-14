using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using XCommon.Application.Login;
using XCommon.Application.Login.Entity;
using XCommon.Extensions.String;
using XCommon.Patterns.Ioc;
using XCommon.Web.Authentication.Base;

namespace XCommon.Web.Authentication.Google
{
    internal class AuthenticationGoogle : AuthenticationBase<AuthenticationGoogleConfig>
    {
        private ILoginBusiness LoginBusiness => Kernel.Resolve<ILoginBusiness>();

        public AuthenticationGoogle(AuthenticationGoogleConfig config)
            : base(config)
        {
        }

        internal override void Register(IApplicationBuilder app)
        {
            if (Config == null)
                return;

            GoogleOptions result = new GoogleOptions
            {
                ClientId = Config.ClientId,
                ClientSecret = Config.ClientSecret,
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
                        return ProcessTicket(ctx);
                    },
                    OnTicketReceived = ctx =>
                    {
                        return Task.FromResult(0);
                    }
                }
            };

            app
                .UseGoogleAuthentication(result);
        }

        private async Task ProcessTicket(OAuthCreatingTicketContext ctx)
        {
            SignUpExternalEntity signUp = GetSignFromGoogle(ctx);
            var ticket = await LoginBusiness.SignUpAsync(signUp);

            if (ticket.HasErro || ticket.Entity.Status != TicketStatus.Sucess)
            {
                ctx.Ticket = null;
                return;
            }

            ctx.Ticket = await TicketManager.GetTicketAsync(ticket.Entity);
        }

        private static SignUpExternalEntity GetSignFromGoogle(OAuthCreatingTicketContext ctx)
        {
            SignUpExternalEntity signUp = new SignUpExternalEntity
            {
                Provider = UserProvider.Google
            };

            var id = ctx.User.Value<string>("id");
            if (id.IsNotEmpty())
            {
                signUp.Token = id;
                signUp.Identifier = id;
            }

            var name = ctx.User.Value<string>("displayName");
            if (name.IsNotEmpty())
            {
                signUp.Name = name;
            }

            var email = ctx.User["emails"]?[0]?["value"]?.ToString();
            if (email.IsNotEmpty())
            {
                signUp.Email = email;
            }

            var gender = ctx.User.Value<string>("gender");
            if (gender.IsNotEmpty())
            {
                signUp.Male = gender.ToLower() == "male";
            }

            var urlProfile = ctx.User.Value<string>("url");
            if (urlProfile.IsNotEmpty())
            {
                signUp.UrlProfile = urlProfile;
            }

            var urlImage = ctx.User["image"]?["url"]?.ToString();
            if (urlImage.IsNotEmpty())
            {
                signUp.UrlImage = urlImage.Replace("?sz=50", string.Empty);
            }

            var urlCover = ctx.User["cover"]?["coverPhoto"]?["url"]?.ToString();
            if (urlCover.IsNotEmpty())
            {
                signUp.UrlCover = urlCover;
            }

            var language = ctx.User.Value<string>("language");
            if (language.IsNotEmpty())
            {
                signUp.Language = language;
            }

            return signUp;
        }
    }
}
