using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XCommon.Application;
using XCommon.Application.Authentication;
using XCommon.Application.Authentication.Entity;
using XCommon.Extensions.Util;
using XCommon.Patterns.Ioc;
using XCommon.Web;

namespace XCommon.Test.Web.Site
{
    public class Startup
    {
		public IConfigurationRoot Configuration { get; }

		public IApplicationSettings ApplicationSettings { get; }

		public Startup(IHostingEnvironment env)
		{
			Kernel.Map<IApplicationSettings>().To(new ApplicationSettings());

			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.Development.json", optional: true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();
			ApplicationSettings = Configuration.Get<ApplicationSettings>("DoIt");

		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
        {
			services
				.UseXCommonAuthentication(Configuration)
				.AddMvc();
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			app
				.UseXCommonAuthentication(env)
				.UseMvc();			
        }
    }
}
