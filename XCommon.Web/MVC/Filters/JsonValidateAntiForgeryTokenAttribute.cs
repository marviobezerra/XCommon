using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace XCommon.Web.MVC.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class JsonValidateAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        private void ValidateRequestHeader(HttpRequestBase request)
        {
            string cookieToken = request.Cookies[AntiForgeryConfig.CookieName].Value;
            string tokenValue = request.Headers["RequestVerificationToken"];

            AntiForgery.Validate(cookieToken, tokenValue);
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    ValidateRequestHeader(filterContext.HttpContext.Request);
                }
                else
                {
                    AntiForgery.Validate();
                }
            }
            catch (HttpAntiForgeryException)
            {
                throw new HttpAntiForgeryException("Anti forgery token cookie not found");
            }
        }
    }
}
