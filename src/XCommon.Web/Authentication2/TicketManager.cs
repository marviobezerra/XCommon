using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using XCommon.Application;
using XCommon.Application.Authentication;
using XCommon.Application.Authentication.Entity;
using XCommon.Application.Executes;
using XCommon.Extensions.String;
using XCommon.Patterns.Ioc;

namespace XCommon.Web.Authentication2
{
	public class TicketManager : ITicketManager
	{
		[Inject]
		private IApplicationSettings ApplicationSettings { get; set; }

		private string CookieCulture => "culture";

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
			var userIdentity = new ClaimsIdentity("SuperSecureLogin");

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Country, signUpTicket.Culture, ApplicationSettings.Name),
				new Claim(ClaimTypes.Name, signUpTicket.Name, ClaimValueTypes.String, ApplicationSettings.Name),
				new Claim(ClaimTypes.NameIdentifier, signUpTicket.Key.ToString(), ClaimValueTypes.String, ApplicationSettings.Name)
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
				var result = Guid.Empty;

				if (!IsAuthenticated)
				{
					return result;
				}

				var identifier = HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

				if (identifier.IsEmpty())
				{
					return result;
				}

				Guid.TryParse(identifier, out result);

				return result;
			}
		}

		public ExecuteUser User
		{
			get
			{
				var userKey = UserKey;

				if (userKey == Guid.Empty)
				{
					return null;
				}

				return new ExecuteUser
				{
					Key = userKey,
					Name = HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value
				};
			}
		}

		public string Culture
		{
			get
			{
				if (!IsAuthenticated)
				{
					return HttpContextAccessor.HttpContext.Request
						?.Cookies
						?.Where(c => c.Key == CookieCulture)
						?.FirstOrDefault()
						.Value ?? ApplicationSettings.Culture.Name;
				}

				return HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country)?.Value;
			}
		}

		public async Task<AuthenticationTicket> GetTicketAsync(TicketEntity signUpTicket)
		{
			return await Task.Run(() =>
			{
				return new AuthenticationTicket(GetClaimsPrincipal(signUpTicket), GetAuthenticationProperties(signUpTicket), CookieAuthenticationDefaults.AuthenticationScheme);
			});
		}

		public async Task<JwtSecurityToken> GetTokenAsync(TicketEntity signUpTicket)
		{
			var authenticationConfig = ApplicationSettings.GetValue<AuthenticationConfig>("Authentication");


			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, signUpTicket.Key.ToString()),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfig.SecurityKey));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(ApplicationSettings.Name, ApplicationSettings.Name, claims, expires: DateTime.Now.AddDays(authenticationConfig.Expiration), signingCredentials: credentials);
			return await Task.FromResult(token);
		}

		public async Task SignInAsync(TicketEntity signUpTicket)
		{
			await HttpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, GetClaimsPrincipal(signUpTicket), GetAuthenticationProperties(signUpTicket));
		}

		public async Task SignOutAsync()
		{
			await HttpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

		}

		public bool SetCookieCulture(string culture)
		{
			if (!ApplicationSettings.Cultures.Any(c => c.Name == culture))
			{
				return false;
			}

			try
			{
				HttpContextAccessor.HttpContext.Response.Cookies.Append(CookieCulture, culture);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
