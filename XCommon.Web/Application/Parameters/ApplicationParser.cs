using System.IO;
using XCommon.Application.CommandLine;
using XCommon.Extensions.Converters;
using XCommon.Patterns.Ioc;

namespace XCommon.Web.Application.Parameters
{
    internal static class ApplicationParser
    {
        internal static IServiceParameters Parser(string serviceName, int defaultPort, string[] args)
        {
            ServiceParameters result = new ServiceParameters
            {
                Name = serviceName
            };

            var webApp = new CommandLineApplication(false);

            var install = webApp.Option("-i|--install", "Install as service", CommandOptionType.NoValue);
            var uninstall = webApp.Option("-u|--uninstall", "Uninstall service", CommandOptionType.NoValue);
            var application = webApp.Option("-a|--application", "Run as application", CommandOptionType.NoValue);
            var service = webApp.Option("-s|--service", "Run as service", CommandOptionType.NoValue);

            var port = webApp.Option("-p|--port", "HTTP port", CommandOptionType.SingleValue);
            var contentPath = webApp.Option("-c|--content", "http content path", CommandOptionType.SingleValue);

            var help = webApp.HelpOption("-h|--help");

            webApp.OnExecute(() =>
            {

                result.ContentPath = contentPath.HasValue() ? contentPath.Value() : Directory.GetCurrentDirectory();
                result.HttpPort = port.HasValue() ? port.Value().ToInt32() : defaultPort.ToInt32();

                if (install.HasValue())
                {
                    result.ServiceInstall = true;
                    return 0;
                }

                if (uninstall.HasValue())
                {
                    result.ServiceUninstall = true;
                    return 0;
                }

                if (application.HasValue())
                {
                    result.RunApplication = true;
                    return 0;
                }

                if (service.HasValue())
                {
                    result.RunService = true;
                    return 0;
                }

                if (help.HasValue())
                {
                    webApp.ShowHelp();
                    return 0;
                }

                result.RunApplication = !webApp.OptionHelpShowed;
                return 0;
            });

            var x = webApp.Execute(args);

            result.ShowHelp = webApp.OptionHelpShowed;

            Kernel.Map<IServiceParameters>().To(result);

            return result;
        }
    }
}
