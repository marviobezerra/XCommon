using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace XCommon.Web.Application
{
    public class ApplicationRun<TStartup>
        where TStartup : class
    {
        public static void Run(int httpPort, string[] args)
        {
            Run(httpPort, Directory.GetCurrentDirectory(), args);
        }

        public static void Run(int httpPort, string contentPath, string[] args)
        {
#if NET451
            if (args.Contains("--service"))
            {
                var exePath = Process.GetCurrentProcess().MainModule.FileName;
                var directoryPath = Path.GetDirectoryName(exePath);
                var host = new WebHostBuilder()
                                .UseUrls($"http://+:{httpPort}")
                                .UseKestrel()
                                .UseContentRoot(directoryPath)
                                .UseStartup<TStartup>()
                                .Build();
                host
                    .RunAsCustomService();
            }
            else
            {
                var host = new WebHostBuilder()
                                .UseUrls($"http://+:{httpPort}")
                                .UseKestrel()
                                .UseContentRoot(contentPath)
                                .UseIISIntegration()
                                .UseStartup<TStartup>()
                                .Build();

                host.Run();
            }
#else
            var host = new WebHostBuilder()
                                .UseUrls($"http://+:{httpPort}")
                                .UseKestrel()
                                .UseContentRoot(contentPath)
                                .UseIISIntegration()
                                .UseStartup<TStartup>()
                                .Build();
            
            host.Run();
#endif
        }
    }
}
