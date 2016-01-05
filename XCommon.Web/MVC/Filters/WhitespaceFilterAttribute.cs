using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace XCommon.Web.MVC.Filters
{
    public class WhitespaceFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!IsEnabled())
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            var request = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;

            response.Filter = new WhiteSpaceFilter(response.Filter, s =>
            {
                s = Regex.Replace(s, @"\s+", " ");
                s = Regex.Replace(s, @"\s*\n\s*", "\n");
                s = Regex.Replace(s, @"\s*\>\s*\<\s*", "><");
                s = Regex.Replace(s, @"<!--(.*?)-->", "");   //Remove comments

                // single-line doctype must be preserved 
                var firstEndBracketPosition = s.IndexOf(">");
                if (firstEndBracketPosition >= 0)
                {
                    s = s.Remove(firstEndBracketPosition, 1);
                    s = s.Insert(firstEndBracketPosition, ">");
                }
                return s;
            });
        }

        private bool IsEnabled()
        {
            if (HttpContext.Current.IsDebuggingEnabled)
                return false;

            string config = ConfigurationManager.AppSettings["XCommon:Filters"];

            if (string.IsNullOrEmpty(config))
                return false;

            if (config.Contains("Whitespace"))
                return true;

            return false;
        }

    }
}
