using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace XCommon.Web.Authentication
{
    public static class AuthenticationExtension
    {
        private static AuthenticationRun Run { get; set; }

        public static IApplicationBuilder UseXCommonAuthentication(this IApplicationBuilder app, IConfigurationRoot config)
        {
            Run = new AuthenticationRun(config);
            return Run.Configure(app);
        }

        public static IApplicationBuilder UseXCommonAuthentication(this IApplicationBuilder app, IHostingEnvironment env)
        {
            Run = new AuthenticationRun(env);
            return Run.Configure(app);
        }
    }
}
