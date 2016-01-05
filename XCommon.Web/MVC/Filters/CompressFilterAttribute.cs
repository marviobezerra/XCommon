using System.Configuration;
using System.IO.Compression;
using System.Web;
using System.Web.Mvc;

namespace XCommon.Web.MVC.Filters
{
    public class CompressFilterAttribute : ActionFilterAttribute
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

            if (IsGZipSupported())
            {
                string AcceptEncoding = request.Headers["Accept-Encoding"];

                if (AcceptEncoding.Contains("gzip"))
                {
                    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                    response.Headers.Remove("Content-Encoding");
                    response.AppendHeader("Content-Encoding", "gzip");
                }
                else
                {
                    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                    response.Headers.Remove("Content-Encoding");
                    response.AppendHeader("Content-Encoding", "deflate");
                }
            }

            response.AppendHeader("Vary", "Content-Encoding");
        }

        private bool IsGZipSupported()
        {
            string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];

            if (!string.IsNullOrEmpty(AcceptEncoding) && (AcceptEncoding.Contains("gzip") || AcceptEncoding.Contains("deflate")))
                return true;

            return false;
        }

        private bool IsEnabled()
        {
            if (HttpContext.Current.IsDebuggingEnabled)
                return false;

            string config = ConfigurationManager.AppSettings["Prospect:Filters"];

            if (string.IsNullOrEmpty(config))
                return false;

            if (config.Contains("Compress"))
                return true;

            return false;
        }
    }
}
