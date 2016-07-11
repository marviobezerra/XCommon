using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System.IO;
using XCommon.Web.Application.Parameters;
using System;
#if NET451
using System.Configuration.Install;
#endif

namespace XCommon.Web.Application
{
    public class ApplicationRun<TStartup>
        where TStartup : class
    {

        public static void Run(string serviceName, string serviceDisplayName, string serviceDescription, int defaultPort, string[] args)
        {
            var parameters = ApplicationParser.Parser(serviceName, defaultPort, args);

            if (parameters.ShowHelp)
                return;

#if NET451
            if (parameters.ServiceInstall)
            {
                RunSC($"create {serviceName} binPath= \"{Process.GetCurrentProcess().MainModule.FileName} -s -p {parameters.HttpPort}\" DisplayName= \"{serviceDisplayName}\" start= auto");
                Console.WriteLine($" - Service {serviceDescription} installer");
                Console.WriteLine($" - Service {serviceDescription} listening on port {parameters.HttpPort}");

                RunSC($"description {serviceName} \"{serviceDescription}\"");

                RunSC($"start {serviceName}");
                Console.WriteLine($" - Service {serviceDescription} started");

                return;
            }

            if (parameters.ServiceUninstall)
            {
                RunSC($"stop {serviceName}");
                Console.WriteLine($" - Service {serviceDescription} stoped");

                RunSC($"delete {serviceName}");
                Console.WriteLine($" - Service {serviceDescription} removed");

                return;
            }

            if (parameters.RunService)
            {
                var exePath = Process.GetCurrentProcess().MainModule.FileName;

                var directoryPath = Path.GetDirectoryName(exePath);
                var host = new WebHostBuilder()
                                .UseUrls($"http://+:{parameters.HttpPort}")
                                .UseKestrel()
                                .UseContentRoot(directoryPath)
                                .UseStartup<TStartup>()
                                .Build();
                host
                    .RunAsCustomService();

                return;
            }

            if (parameters.RunApplication)
            {
                var host = new WebHostBuilder()
                                .UseUrls($"http://+:{parameters.HttpPort}")
                                .UseKestrel()
                                .UseContentRoot(parameters.ContentPath)
                                .UseIISIntegration()
                                .UseStartup<TStartup>()
                                .Build();

                host.Run();

                return;
            }
#else
            var host = new WebHostBuilder()
                                .UseUrls($"http://+:{parameters.HttpPort}")
                                .UseKestrel()
                                .UseContentRoot(parameters.ContentPath)
                                .UseIISIntegration()
                                .UseStartup<TStartup>()
                                .Build();
            
            host.Run();
#endif
        }

#if NET451
        public static bool RunSC(string parameters)
        {
            var scPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "System32", "sc.exe");

            if (!File.Exists(scPath))
            {
                Console.WriteLine("SC Not Found");
                return false;
            }

            var install = new Process();
            install.StartInfo.FileName = scPath;
            install.StartInfo.Arguments = parameters;
            install.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            install.Start();
            install.WaitForExit();

            return true;
        }
#endif
    }
}
