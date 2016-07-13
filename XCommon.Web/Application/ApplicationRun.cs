using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System.IO;
using XCommon.Web.Application.Parameters;
using System;
using XCommon.Web.Application.WindowsService;
#if NET451
using System.Configuration.Install;
#endif

namespace XCommon.Web.Application
{
    public class ApplicationRun<TStartup>
        where TStartup : class
    {
        public static void Run(ServiceParameters parameter, string[] args)
        {
            ApplicationParser.Parser(parameter, args);

            if (parameter.ShowHelp)
                return;

#if NET451
            if (parameter.ServiceInstall)
            {
                ApplicationHostServiceHelper.Install(parameter);
                return;
            }

            if (parameter.ServiceUninstall)
            {
                ApplicationHostServiceHelper.Uninstall(parameter);
                return;
            }

            if (parameter.RunService)
            {
                var exePath = Process.GetCurrentProcess().MainModule.FileName;

                var directoryPath = Path.GetDirectoryName(exePath);
                var host = new WebHostBuilder()
                                .UseUrls($"http://+:{parameter.HttpPort}")
                                .UseKestrel()
                                .UseContentRoot(directoryPath)
                                .UseStartup<TStartup>()
                                .Build();
                host
                    .RunAsCustomService();

                return;
            }

            if (parameter.RunApplication)
            {
                var host = new WebHostBuilder()
                                .UseUrls($"http://+:{parameter.HttpPort}")
                                .UseKestrel()
                                .UseContentRoot(parameter.ContentPath)
                                .UseIISIntegration()
                                .UseStartup<TStartup>()
                                .Build();

                host.Run();

                return;
            }
#else
            var host = new WebHostBuilder()
                                .UseUrls($"http://+:{parameter.HttpPort}")
                                .UseKestrel()
                                .UseContentRoot(parameter.ContentPath)
                                .UseIISIntegration()
                                .UseStartup<TStartup>()
                                .Build();
            
            host.Run();
#endif
        }
    }
}
