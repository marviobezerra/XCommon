using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using XCommon.Application;
using XCommon.Application.Authentication.Entity;
using XCommon.Extensions.String;
using XCommon.Extensions.Util;
using XCommon.Patterns.Ioc;
using XCommon.Web.Authentication;
using XCommon.Web.Authentication.Providers;

namespace XCommon.Web.Extension
{
	internal class AuthenticationRun
	{
		internal AuthenticationRun(IConfigurationRoot config)
		{
			Configuration = config;
			ApplicationSettings = Kernel.Resolve<IApplicationSettings>();
			AuthenticationConfig = Configuration.Get<AuthenticationConfig>(ApplicationBuilderExtension.Authentication);
			ApplicationSettings.Values.Put(ApplicationBuilderExtension.Authentication, AuthenticationConfig);

			SetUp();
		}

		private IConfigurationRoot Configuration { get; }

		private List<BaseProvider> Providers { get; set; }

		private IApplicationSettings ApplicationSettings { get; set; }

		private AuthenticationConfig AuthenticationConfig { get; set; }


		private void SetUp()
		{
			Providers = new List<BaseProvider>();

			if (AuthenticationConfig.Facebook != null)
				Providers.Add(AuthenticationConfig.Facebook);

			if (AuthenticationConfig.Google != null)
				Providers.Add(AuthenticationConfig.Google);

			if (AuthenticationConfig.Local != null)
				Providers.Add(AuthenticationConfig.Local);
		}

		internal IServiceCollection Configure(IServiceCollection services)
		{
			var authSchema = AuthenticationConfig.AuthenticationType == AuthenticationType.Token
				? JwtBearerDefaults.AuthenticationScheme
				: CookieAuthenticationDefaults.AuthenticationScheme;

			var auth = services.AddAuthentication(authSchema);
			var audience = ApplicationSettings.Name.IsEmpty() ? "XCommon" : ApplicationSettings.Name;

			if (AuthenticationConfig.AuthenticationType == AuthenticationType.Token)
			{
				auth.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, cfg =>
				{
					cfg.RequireHttpsMetadata = false;
					cfg.SaveToken = true;

					cfg.Events = new JwtBearerEvents
					{
						OnAuthenticationFailed = ex =>  {
							var x = ex;
							return Task.FromResult(0);
						}
					};

					cfg.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidIssuer = audience,
						ValidAudience = audience,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthenticationConfig.SecurityKey))
					};

				});
			}

			if (AuthenticationConfig.AuthenticationType == AuthenticationType.Cookie)
			{
				auth.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, cfg =>
				{
					cfg.SlidingExpiration = true;
					cfg.Cookie.HttpOnly = false;
					cfg.Cookie.Name = audience;
				});
			}

			Providers.ForEach(provider => provider.SetUp(auth));
			return services;
		}

		internal IApplicationBuilder Configure(IApplicationBuilder app)
		{
			var applicationSettings = Kernel.Resolve<IApplicationSettings>();
			var config = Configuration.Get<AuthenticationConfig>(ApplicationBuilderExtension.Authentication);

			app
			   .UseAuthentication()
			   .Map(config.UriLogin, signoutApp =>
			   {
				   signoutApp.Run(async context =>
				   {
					   var authType = context.Request.Query["scheme"];

					   if (!string.IsNullOrEmpty(authType))
					   {
						   context.Request.PathBase = new PathString(config.UriApplicationBase);
						   await context.ChallengeAsync(authType, new AuthenticationProperties() { RedirectUri = "/" });
						   return;
					   }

					   context.Response.ContentType = "text/html";
					   await context.Response.WriteAsync("<html><body>");
					   await context.Response.WriteAsync("Choose an authentication scheme: <br>");
					   foreach (var type in Providers)
					   {
						   await context.Response.WriteAsync($"<a href='?scheme={type.Provider}'>{type.Provider}</a><br>");
					   }

					   await context.Response.WriteAsync("</body></html>");
				   });
			   });

			return app;
		}
	}
}
