using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using XCommon.Application.Login;
using XCommon.Extensions.String;

namespace XCommon.Web.Authentication.Ticket
{
    public class TicketManager : ITicketManager, ITicketManagerWeb
    {
        const string Issuer = "http://www.mypetlife.com";
        private IHttpContextAccessor HttpContextAccessor { get; set; }

        public TicketManager(IHttpContextAccessor accessor)
        {
            HttpContextAccessor = accessor;
        }

        #region Private
        private AuthenticationProperties GetAuthenticationProperties(TicketEntity signUpTicket)
        {
            return new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(3600),
                IsPersistent = true,
                AllowRefresh = true
            };
        }

        private ClaimsPrincipal GetClaimsPrincipal(TicketEntity signUpTicket)
        {
            ClaimsIdentity userIdentity = new ClaimsIdentity("SuperSecureLogin");

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, signUpTicket.Name, ClaimValueTypes.String, Issuer),
                new Claim(ClaimTypes.NameIdentifier, signUpTicket.Key.ToString(), ClaimValueTypes.String, Issuer)
            };

            userIdentity.AddClaims(claims);

            return new ClaimsPrincipal(userIdentity);
        }
        #endregion

        public bool IsAuthenticated
        {
            get
            {
                return HttpContextAccessor.HttpContext.User.Identities.Any(c => c.IsAuthenticated);
            }
        }

        public Guid UserKey
        {
            get
            {
                Guid result = Guid.Empty;

                if (!IsAuthenticated)
                    return result;

                var identifier = HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (identifier.IsEmpty())
                    return result;

                Guid.TryParse(identifier, out result);

                return result;
            }
        }

        public async Task<AuthenticationTicket> GetTicketAsync(TicketEntity signUpTicket)
        {
            return await Task.Run(() =>
            {
                return new AuthenticationTicket(GetClaimsPrincipal(signUpTicket), GetAuthenticationProperties(signUpTicket), CookieAuthenticationDefaults.AuthenticationScheme);
            });
        }

        public async Task SignInAsync(TicketEntity signUpTicket)
        {
            await HttpContextAccessor.HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, GetClaimsPrincipal(signUpTicket), GetAuthenticationProperties(signUpTicket));
        }

        public async Task SignOutAsync()
        {
            await HttpContextAccessor.HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        }
    }
}
