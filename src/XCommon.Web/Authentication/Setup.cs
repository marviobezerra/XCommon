using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using XCommon.Application;
using XCommon.Application.Settings;
using XCommon.Patterns.Ioc;

namespace XCommon.Web.Authentication
{
	public static class Setup
    {
		public static IServiceCollection SetupJwtToken(this IServiceCollection services)
		{
			var appSettings = Kernel.Resolve<IApplicationSettings>();

			if (appSettings.Authentication == null)
			{
				throw new Exception("Authentication parameters missing on ApplicationSettings");
			}

			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

			services.AddAuthentication(options =>
			 {
				 options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				 options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				 options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			 })
				.AddJwtBearer(cfg =>
				{
					cfg.RequireHttpsMetadata = false;
					cfg.SaveToken = true;
					cfg.TokenValidationParameters = new TokenValidationParameters
					{
						ValidIssuer = appSettings.Authentication.Issuer,
						ValidAudience = appSettings.Authentication.Audience,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Authentication.SecurityKey)),
						ClockSkew = TimeSpan.Zero,
						NameClaimType = JwtRegisteredClaimNames.Sub,
						RoleClaimType = "roles"
					};
				});

			return services;
		}
    }
}
