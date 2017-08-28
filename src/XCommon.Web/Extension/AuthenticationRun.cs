using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using XCommon.Application;
using XCommon.Extensions.Util;
using XCommon.Patterns.Ioc;
using XCommon.Web.Authentication2;
using XCommon.Web.Authentication2.Providers;

namespace XCommon.Web.Extension
{
	internal class AuthenticationRun
	{
		internal AuthenticationRun(IConfigurationRoot config)
		{
			Configuration = config;
			SetUp();
		}

		internal AuthenticationRun(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
			   .SetBasePath(env.ContentRootPath)
			   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
			   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

			builder.AddEnvironmentVariables();
			Configuration = builder.Build();
			SetUp();
		}

		private IConfigurationRoot Configuration { get; }

		private List<BaseProvider> Authenticators { get; set; }

		private void SetUp()
		{
			Authenticators = new List<BaseProvider>();

			var applicationSettings = Kernel.Resolve<IApplicationSettings>();
			var config = Configuration.Get<AuthenticationConfig>("XCommon:Authentication");

			applicationSettings.SetValue("Authentication", config);

			if (config.Facebook != null)
				Authenticators.Add(config.Facebook);

			if (config.Google != null)
				Authenticators.Add(config.Google);

			if (config.Local != null)
				Authenticators.Add(config.Local);
		}

		internal AuthenticationBuilder Configure(AuthenticationBuilder builder)
		{
			Authenticators.ForEach(auth => auth.SetUp(builder));
			return builder;
		}

		internal IApplicationBuilder Configure(IApplicationBuilder app)
		{
			var applicationSettings = Kernel.Resolve<IApplicationSettings>();
			var config = Configuration.Get<AuthenticationConfig>("XCommon:Authentication");

			app
			   .Map(config.UriLogin, signoutApp =>
			   {
				   signoutApp.Run(async context =>
				   {
					   var authType = context.Request.Query["scheme"];

					   if (!string.IsNullOrEmpty(authType))
					   {
						   context.Request.PathBase = new PathString("");
						   await context.ChallengeAsync(authType, new AuthenticationProperties() { RedirectUri = "/" });
						   return;
					   }

					   context.Response.ContentType = "text/html";
					   await context.Response.WriteAsync("<html><body>");
					   await context.Response.WriteAsync("Choose an authentication scheme: <br>");
					   foreach (var type in Authenticators)
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
