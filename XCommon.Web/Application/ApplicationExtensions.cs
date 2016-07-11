#if NET451
using Microsoft.AspNetCore.Hosting;
using System.ServiceProcess;
#endif

namespace XCommon.Web.Application
{
#if NET451
    public static class ApplicationExtensions
    {
        public static void RunAsCustomService(this IWebHost host)
        {
            var webHostService = new ApplicationHostService(host);
            ServiceBase.Run(webHostService);
        }
    }
#endif
}
