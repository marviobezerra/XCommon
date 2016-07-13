using System;
using System.Diagnostics;
using System.IO;
using XCommon.Web.Application.Parameters;

namespace XCommon.Web.Application.WindowsService
{
#if NET451
    internal static class ApplicationHostServiceHelper
    {
        private static bool RunSC(string parameters)
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

        internal static bool Install(ServiceParameters parameter)
        {
            RunSC($"create {parameter.Name} binPath= \"{Process.GetCurrentProcess().MainModule.FileName} -s -p {parameter.HttpPort}\" DisplayName= \"{parameter.DisplayName}\" start= auto");
            Console.WriteLine($" - Service {parameter.DisplayName} installer");
            Console.WriteLine($" - Service {parameter.DisplayName} listening on port {parameter.HttpPort}");

            RunSC($"description {parameter.Name} \"{parameter.Description}\"");

            RunSC($"start {parameter.Name}");
            Console.WriteLine($" - Service {parameter.DisplayName} started");

            Process.Start($"http://localhost:{parameter.HttpPort}");

            return true;
        }

        internal static bool Uninstall(ServiceParameters parameter)
        {
            RunSC($"stop {parameter.Name}");
            Console.WriteLine($" - Service {parameter.DisplayName} stoped");

            RunSC($"delete {parameter.Name}");
            Console.WriteLine($" - Service {parameter.DisplayName} removed");
            
            return true;
        }
    }
#endif
}
