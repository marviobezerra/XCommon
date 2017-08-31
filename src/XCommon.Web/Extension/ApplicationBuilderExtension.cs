using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XCommon.Web.Extension;

namespace XCommon.Web
{
	public static class ApplicationBuilderExtension
	{
		private static AuthenticationRun Run { get; set; }

		internal static string Authentication => "XCommon:Authentication";

		public static IServiceCollection UseXCommonAuthentication(this IServiceCollection services, IConfigurationRoot config)
		{
			if (Run == null)
				Run = new AuthenticationRun(config);

			Run.Configure(services);

			return services;
		}

		public static IApplicationBuilder UseXCommonAuthentication(this IApplicationBuilder app, IConfigurationRoot config)
		{
			if (Run == null)
				Run = new AuthenticationRun(config);

			Run
				.Configure(app);

			return app;
		}
	}
}
