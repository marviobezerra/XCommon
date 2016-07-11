using System.IO;
using XCommon.Application.CommandLine;
using XCommon.Extensions.Converters;

namespace XCommon.Web.Application.Parameters
{
    internal static class ApplicationParser
    {
        internal static void Parser(ServiceParameters parameter, string[] args)
        {
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

                parameter.ContentPath = contentPath.HasValue() ? contentPath.Value() : Directory.GetCurrentDirectory();
                parameter.HttpPort = port.HasValue() ? port.Value().ToInt32() : parameter.HttpPort;

                if (install.HasValue())
                {
                    parameter.ServiceInstall = true;
                    return 0;
                }

                if (uninstall.HasValue())
                {
                    parameter.ServiceUninstall = true;
                    return 0;
                }

                if (application.HasValue())
                {
                    parameter.RunApplication = true;
                    return 0;
                }

                if (service.HasValue())
                {
                    parameter.RunService = true;
                    return 0;
                }

                if (help.HasValue())
                {
                    webApp.ShowHelp();
                    return 0;
                }

                parameter.RunApplication = !webApp.OptionHelpShowed;
                return 0;
            });

            var x = webApp.Execute(args);

            parameter.ShowHelp = webApp.OptionHelpShowed;            
        }
    }
}
