using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using XCommon.Patterns.Ioc;

namespace XCommon.Web.Test
{
    public class Startup
    {
        public XCommon.Application.Logger.ILogger Logger { get; set; }

        public Startup(IHostingEnvironment env)
        {
            Map();
            Logger = Kernel.Resolve<XCommon.Application.Logger.ILogger>();

            Logger.Log(Logger.GetInfo(), "App starting");

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Logger.Log(Logger.GetInfo(), "Start ConfigureServices");

            // Add framework services.
            services.AddMvc();

            Logger.Log(Logger.GetInfo(), "End ConfigureServices");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Logger.Log(Logger.GetInfo(), "Start Configure");


            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            Logger.Log(Logger.GetInfo(), "End Configure");

        }

        private void Map()
        {
            Kernel.Map<XCommon.Application.Logger.ILoggerFormatter>().To<XCommon.Application.Logger.Implementations.LoggerFormatterStandard>();
            Kernel.Map<XCommon.Application.Logger.ILogger>().To<XCommon.Application.Logger.Implementations.LoggerFile>(@"D:\Test\Log.txt");
        }
    }
}
