using Microsoft.AspNetCore.Mvc;
using XCommon.Application.Login;
using XCommon.Patterns.Ioc;

namespace XCommon.Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            Kernel.Resolve(this);
        }

        [Inject]
        protected ITicketManager Ticket { get; private set; }
    }
}
