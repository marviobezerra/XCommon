using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using XCommon.Application;
using XCommon.Application.Authentication;
using XCommon.Extensions.Util;
using XCommon.Patterns.Ioc;
using XCommon.Web.Authentication;

namespace XCommon.Test.Web.Site
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public IApplicationSettings ApplicationSettings { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			ApplicationSettings = Configuration.GetSection("XCommon").Get<ApplicationSettings>();
			Kernel.Map<IApplicationSettings>().To(ApplicationSettings);
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services
				.SetupJwtToken()
				.AddMvc()
				.AddJsonOptions(options =>
				{
					options.SerializerSettings.ContractResolver = new DefaultContractResolver();
				});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
			Kernel.Map<ITicketManager>().To<TicketManager>(httpContextAccessor);

			app
				.UseAuthentication()
				.UseMvc();
		}
	}
}
