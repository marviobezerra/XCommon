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

        public static void Run(ServiceParameters parameter, string[] args)
        {
            ApplicationParser.Parser(parameter, args);

            if (parameter.ShowHelp)
                return;

#if NET451
            if (parameter.ServiceInstall)
            {
                RunSC($"create {parameter.Name} binPath= \"{Process.GetCurrentProcess().MainModule.FileName} -s -p {parameter.HttpPort}\" DisplayName= \"{parameter.DisplayName}\" start= auto");
                Console.WriteLine($" - Service {parameter.DisplayName} installer");
                Console.WriteLine($" - Service {parameter.DisplayName} listening on port {parameter.HttpPort}");

                RunSC($"description {parameter.Name} \"{parameter.Description}\"");

                RunSC($"start {parameter.Name}");
                Console.WriteLine($" - Service {parameter.DisplayName} started");

                Process.Start($"http://localhost:{parameter.HttpPort}");

                return;
            }

            if (parameter.ServiceUninstall)
            {
                RunSC($"stop {parameter.Name}");
                Console.WriteLine($" - Service {parameter.DisplayName} stoped");

                RunSC($"delete {parameter.Name}");
                Console.WriteLine($" - Service {parameter.DisplayName} removed");

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
