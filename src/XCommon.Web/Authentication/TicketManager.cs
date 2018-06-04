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
		private string ClaimCulture => "culture";

		[Inject]
		private IApplicationSettings ApplicationSettings { get; set; }

		private IHttpContextAccessor HttpContextAccessor { get; set; }

		public TicketManager(IHttpContextAccessor accessor)
		{
			Kernel.Resolve(this);
			HttpContextAccessor = accessor;
		}

		public string WriteToken(TicketEntity ticket)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, ticket.Name),
				new Claim(JwtRegisteredClaimNames.Jti, ticket.Key.ToString()),
				new Claim(ClaimCulture, ticket.Culture)
			};

			ticket.Roles.ForEach(role =>
			{
				claims.Add(new Claim("roles", role));
			});

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ApplicationSettings.Authentication.SecurityKey));
			var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var audience = ApplicationSettings.Authentication.Audience;
			var issuer = ApplicationSettings.Authentication.Issuer;
			var expiration = DateTime.Now.AddDays(ApplicationSettings.Authentication.Expiration);

			var result = new JwtSecurityToken(issuer, audience, claims, expires: expiration, signingCredentials: signingCredentials);
			return new JwtSecurityTokenHandler().WriteToken(result);
		}

		public bool IsAuthenticated
		{
			get
			{
				return HttpContextAccessor.HttpContext.User.Identities.Any(c => c.IsAuthenticated);
			}
		}

		public string Name
		{
			get
			{
				if (!IsAuthenticated)
				{
					return string.Empty;
				}


				return HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
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
				

				var identifier = HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

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
				if (!IsAuthenticated)
				{
					return null;
				}

				return new ExecuteUser
				{
					Name = Name,
					UserKey = UserKey,
					Culture = Culture
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
	}
}
