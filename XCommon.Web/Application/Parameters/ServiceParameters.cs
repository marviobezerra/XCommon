using System;

namespace XCommon.Web.Application.Parameters
{
    public class ServiceParameters
    {

        public int HttpPort { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string ContentPath { get; set; }

        public bool ServiceInstall { get; set; }

        public bool ServiceUninstall { get; set; }

        public bool RunApplication { get; set; }

        public bool RunService { get; set; }
        
        public bool ShowHelp { get; set; }
    }
}
