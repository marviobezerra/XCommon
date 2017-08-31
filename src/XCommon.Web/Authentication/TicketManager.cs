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

namespace XCommon.Web.Authentication
{
	public class TicketManager : ITicketManager
	{
		private string ClaimCulture => "Culture";

		[Inject]
		private IApplicationSettings ApplicationSettings { get; set; }

		private AuthenticationConfig AuthenticationConfig { get; set; }

		private IHttpContextAccessor HttpContextAccessor { get; set; }

		private Dictionary<string, TokenEntity> Tokens { get; set; }

		public TicketManager(IHttpContextAccessor accessor)
		{
			Kernel.Resolve(this);
			Tokens = new Dictionary<string, TokenEntity>();
			AuthenticationConfig = ApplicationSettings.Values.Get<AuthenticationConfig>(ApplicationBuilderExtension.Authentication);
			HttpContextAccessor = accessor;

		}

		#region Private
		private AuthenticationProperties GetAuthenticationProperties(TicketEntity signUpTicket)
		{
			return new AuthenticationProperties
			{
				ExpiresUtc = DateTime.UtcNow.AddDays(AuthenticationConfig.Expiration),
				IsPersistent = true,
				AllowRefresh = true
			};
		}

		private ClaimsPrincipal GetClaimsPrincipal(TicketEntity signUpTicket)
		{
			var audience = ApplicationSettings.Name.IsEmpty() ? "XCommon" : ApplicationSettings.Name;

			var userIdentity = new ClaimsIdentity(audience);
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, signUpTicket.Key.ToString()),
				new Claim(ClaimTypes.Name, signUpTicket.Name),
				new Claim(ClaimCulture, signUpTicket.Culture)
			};

			userIdentity.AddClaims(claims);

			return new ClaimsPrincipal(userIdentity);
		}

		private JwtSecurityToken GetTokenAsync(TicketEntity signUpTicket)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, signUpTicket.Key.ToString()),
				new Claim(JwtRegisteredClaimNames.Jti, signUpTicket.Name),
				new Claim(ClaimCulture, signUpTicket.Culture)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthenticationConfig.SecurityKey));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var audience = ApplicationSettings.Name.IsEmpty() ? "XCommon" : ApplicationSettings.Name;

			return new JwtSecurityToken(audience, audience, claims, expires: DateTime.Now.AddDays(AuthenticationConfig.Expiration), signingCredentials: credentials);
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

				var claimName = AuthenticationConfig.AuthenticationType == AuthenticationType.Cookie
					? ClaimTypes.NameIdentifier
					: ClaimTypes.NameIdentifier;

				var identifier = HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == claimName)?.Value;

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

				var claimName = AuthenticationConfig.AuthenticationType == AuthenticationType.Cookie
					? ClaimTypes.Name
					: JwtRegisteredClaimNames.Jti;

				return new ExecuteUser
				{
					Key = userKey,
					Name = HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == claimName)?.Value,
					Culture = HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimCulture)?.Value
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
						?.Where(c => c.Key == ClaimCulture)
						?.FirstOrDefault()
						.Value ?? ApplicationSettings.Culture.Name;
				}

				return HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimCulture)?.Value;
			}
		}

		public async Task<string> SignInAsync(TicketEntity signUpTicket)
		{
			var key = Util.Functions.GetToken(4, 3, 4);

			if (AuthenticationConfig.AuthenticationType == AuthenticationType.Cookie)
			{
				await HttpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, GetClaimsPrincipal(signUpTicket), GetAuthenticationProperties(signUpTicket));
				HttpContextAccessor.HttpContext.Response.Redirect(AuthenticationConfig.UriCookieSucess);
				return key;
			}

			var token = new TokenEntity
			{
				Culture = signUpTicket.Culture,
				Expire = DateTime.UtcNow.AddDays(AuthenticationConfig.Expiration),
				Name = signUpTicket.Name,
				Token = new JwtSecurityTokenHandler().WriteToken(GetTokenAsync(signUpTicket))
			};

			Tokens.Add(key, token);
			return key;
		}

		public async Task SignOutAsync()
		{
			await HttpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		}

		public async Task<TokenEntity> CheckTokenAsync(string key)
		{
			return await Task.Factory.StartNew(() =>
			{
				key = key.Replace("#", string.Empty);

				if (Tokens.ContainsKey(key))
				{
					var result = Tokens[key];
					Tokens.Remove(key);
					return result;
				}

				return null;
			});
		}
	}
}
