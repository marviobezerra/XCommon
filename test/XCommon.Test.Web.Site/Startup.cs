using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XCommon.Application;
using XCommon.Application.Authentication;
using XCommon.Extensions.Util;
using XCommon.Patterns.Ioc;
using XCommon.Web;
using XCommon.Web.Authentication;

namespace XCommon.Test.Web.Site
{
	public class Startup
	{
		public IConfigurationRoot Configuration { get; }

		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.Development.json", optional: true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();


		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			Kernel.Map<IApplicationSettings>().To(Configuration.Get<ApplicationSettings>("XCommon:Authentication"));
			Kernel.Map<ILoginBusiness>().To<Code.LoginBusiness>();

			services
				.AddCors(options =>
				{
					options.AddPolicy("AllowAllOrigins",
						builder =>
						{
							builder
								.AllowAnyOrigin()
								.AllowAnyHeader()
								.AllowAnyMethod();
						});
				})
				.UseXCommonAuthentication(Configuration);

			services
				.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
			Kernel.Map<ITicketManager>().To<TicketManager>(httpContextAccessor);

			app
				.UseXCommonAuthentication(Configuration)
				.UseDeveloperExceptionPage();

			app.Map("/token", c => {
				c.Run(async context => {
					var key = context.Request.Path.Value.Split("/").Last();
					var token = await Kernel.Resolve<ITicketManager>().CheckTokenAsync(key);
					await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(token));
				});
			});

			app.UseMvc();
		}
	}
}
