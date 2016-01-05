using Microsoft.Owin.Security;
using XCommon.Application.Culture;
using XCommon.Patterns.Ioc;
using XCommon.Patterns.Repository.Executes;
using XCommon.Web.MVC.Filters;
using XCommon.Web.Util;
using System;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace XCommon.Web.MVC.Controllers
{
    [Authorize]
    [CompressFilter]
    public abstract class BaseController : Controller
    {
        public BaseController()
        {
            Kernel.Resolve(this);
        }

        protected IAuthenticationManager Authentication
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return User.Identity.IsAuthenticated;
            }
        }

        public ExecuteUser UserSession
        {
            get
            {
                ExecuteUser retorno;

                if (IsAuthenticated)
                    retorno = new ExecuteUser { Key = (object)User.Identity.Name };
                else
                    retorno = null;

                return retorno;
            }
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            HttpCookie cultureCookie = Request.Cookies["_culture"];

            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null;

            cultureName = CultureHelper.GetImplementedCulture(cultureName);

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        [HttpPost]
        public JsonResult SetCulture(string culture)
        {
            try
            {
                culture = CultureHelper.GetImplementedCulture(culture);

                HttpCookie cookie = Request.Cookies["_culture"];

                if (cookie != null)
                {
                    cookie.Value = culture;
                }
                else
                {
                    cookie = new HttpCookie("_culture");
                    cookie.Value = culture;
                    cookie.Expires = DateTime.Now.AddYears(1);
                }

                Response.Cookies.Add(cookie);

                return Json(true, JsonRequestBehavior.DenyGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.DenyGet);
            }
        }
    }
}
