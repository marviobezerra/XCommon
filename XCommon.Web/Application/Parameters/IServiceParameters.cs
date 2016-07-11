using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommon.Web.Application.Parameters
{
    public interface IServiceParameters
    {
        int HttpPort { get; }

        string Name { get; }

        string ContentPath { get; }

        bool ServiceInstall { get; }

        bool ServiceUninstall { get; }

        bool RunApplication { get; }

        bool RunService { get; }

        bool ShowHelp { get; }
    }
}
