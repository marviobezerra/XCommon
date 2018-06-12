using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using XCommon.Application.Authentication;
using XCommon.Application.Settings;
using XCommon.Extensions.String;
using XCommon.Patterns.Ioc;

namespace XCommon.Web.Controllers
{
	public class BaseController : Controller
    {
        public BaseController()
        {
            Kernel.Resolve(this);

            if (Ticket != null)
            {
                if (Ticket.Culture.IsEmpty())
				{
					return;
				}

#if NET452
				System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(Ticket.Culture);
				System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(Ticket.Culture);
#else
				CultureInfo.CurrentCulture = new CultureInfo(Ticket.Culture);
                CultureInfo.CurrentUICulture = new CultureInfo(Ticket.Culture);
#endif
			}
		}

        [Inject(forceResolve: false)]
        protected ITicketManager Ticket { get; private set; }

        [Inject(forceResolve: false)]
        protected IApplicationSettings ApplicationSettings { get; private set; }
    }
}
