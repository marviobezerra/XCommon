using System.Linq;

namespace XCommon.Web.Test
{
    public class Program : XCommon.Web.Application.ApplicationRun<Startup>
    {
        public static void Main(string[] args)
        {
            //@"D:\VSGit\XCommon\XCommon.Web.Test"
            Run("XCommonWebTest", "XCommon Web Test", "XCommon Web Test - Just a service test", 81, args);
        }
    }
}
