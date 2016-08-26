using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Threading;
using XCommon.Application;
using XCommon.Application.Login;
using XCommon.Extensions.String;
using XCommon.Patterns.Ioc;
using XCommon.Patterns.Repository.Entity;

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
                    return;

#if NET451
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(Ticket.Culture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(Ticket.Culture);
#elif NET46
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(Ticket.Culture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(Ticket.Culture);
#else
                CultureInfo.CurrentCulture = new CultureInfo(Ticket.Culture);
                CultureInfo.CurrentUICulture = new CultureInfo(Ticket.Culture);
#endif
            }
        }

        [Inject]
        protected ITicketManager Ticket { get; private set; }

        [Inject]
        protected IApplicationSettings ApplicationSettings { get; private set; }
    }
}
