#if NET451
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
#endif

namespace XCommon.Web.Application.WindowsService
{
#if NET451
    internal class ApplicationHostService : WebHostService
    {
        public ApplicationHostService(IWebHost host)
            : base(host)
        {
        }

        protected override void OnStarting(string[] args)
        {
            // Log
            base.OnStarting(args);
        }

        protected override void OnStarted()
        {
            // More log
            base.OnStarted();
        }

        protected override void OnStopping()
        {
            // Even more log
            base.OnStopping();
        }
    }
#endif
}
