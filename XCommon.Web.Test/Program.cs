using XCommon.Web.Application;
using XCommon.Web.Application.Parameters;

namespace XCommon.Web.Test
{
    public class Program : XCommon.Web.Application.ApplicationRun<Startup>
    {
        public static void Main(string[] args)
        {
            ServiceParameters parameter = new ServiceParameters
            {
                Name = "XCommonWebTest",
                DisplayName = "XCommon Web Test",
                Description = "XCommon Web Test - Just a service test",
                HttpPort = 81
            };

            Run(parameter, args);
        }
    }
}
