using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using XCommon.Web.Authentication.Base;
using XCommon.Web.Authentication.Cookie;
using XCommon.Web.Authentication.Facebook;
using XCommon.Web.Authentication.Google;
using XCommon.Web.Authentication.Map;

namespace XCommon.Web.Authentication
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

        private List<AuthenticationBase> Authenticators { get; set; }
        
        private void SetUp()
        {
            Authenticators = new List<AuthenticationBase>();
            AuthenticationConfig config = Configuration.Get<AuthenticationConfig>("XCommon:Authentication");

            Authenticators.Add(new AuthenticationCookie(config.Cookie));
            Authenticators.Add(new AuthenticationGoogle(config.Google));
            Authenticators.Add(new AuthenticationFacebook(config.Facebook));
            Authenticators.Add(new AuthenticationMap(config.Map));
        }

        internal IApplicationBuilder Configure(IApplicationBuilder app)
        {
            Authenticators.ForEach(auth => auth.Register(app));

            return app;
        }
    }
}
