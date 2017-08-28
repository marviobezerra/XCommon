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

		

		public static IServiceCollection UseXCommonAuthentication(this IServiceCollection services, IConfigurationRoot config)
		{
			if (Run == null)
				Run = new AuthenticationRun(config);

			Run.Configure(services.AddAuthentication());

			return services;
		}

		public static IServiceCollection UseXCommonAuthentication(this IServiceCollection services, IHostingEnvironment env)
		{
			if (Run == null)
				Run = new AuthenticationRun(env);

			Run.Configure(services.AddAuthentication());

			return services;
		}

		public static IApplicationBuilder UseXCommonAuthentication(this IApplicationBuilder app, IConfigurationRoot config)
        {
			if (Run == null)
				Run = new AuthenticationRun(config);

			Run
				.Configure(app)
				.UseAuthentication();

			return app;
		}

        public static IApplicationBuilder UseXCommonAuthentication(this IApplicationBuilder app, IHostingEnvironment env)
        {
			if (Run == null)
				Run = new AuthenticationRun(env);

			Run
				.Configure(app)
				.UseAuthentication();

			return app;
		}
    }
}
