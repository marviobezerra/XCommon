using Microsoft.AspNetCore.Builder;
using XCommon.Patterns.Ioc;
using XCommon.Web.Authentication.Ticket;

namespace XCommon.Web.Authentication.Base
{
    internal abstract class AuthenticationBase
    {
        internal abstract void Register(IApplicationBuilder app);
    }

    internal abstract class AuthenticationBase<TConfig> : AuthenticationBase
        where TConfig: AuthenticationConfigBase
    {
        public AuthenticationBase(TConfig config)
        {
            Config = config;
        }
        
        protected TConfig Config { get; set; }

        protected ITicketManagerWeb TicketManager => Kernel.Resolve<Ticket.ITicketManagerWeb>();
    }
}
