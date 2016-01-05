using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace XCommon.Web.MVC.Utils
{
    public class ViewRenderer
    {
        protected ControllerContext Context { get; set; }

        public ViewRenderer(ControllerContext controllerContext = null)
        {
            if (controllerContext == null)
            {
                if (HttpContext.Current != null)
                    controllerContext = CreateController<EmptyController>().ControllerContext;
                else
                    throw new InvalidOperationException("ViewRenderer must run in the context of an ASP.NET Application and requires HttpContext.Current to be present.");
            }

            Context = controllerContext;
        }

        public string RenderViewToString(string viewPath, object model = null)
        {
            return RenderViewToStringInternal(viewPath, model, false);
        }

        public string RenderPartialViewToString(string viewPath, object model = null)
        {
            return RenderViewToStringInternal(viewPath, model, true);
        }

        public static string RenderView(string viewPath, object model = null, ControllerContext controllerContext = null)
        {
            ViewRenderer renderer = new ViewRenderer(controllerContext);
            return renderer.RenderViewToString(viewPath, model);
        }

        public static string RenderPartialView(string viewPath, object model = null, ControllerContext controllerContext = null)
        {
            ViewRenderer renderer = new ViewRenderer(controllerContext);
            return renderer.RenderPartialViewToString(viewPath, model);
        }

        private string RenderViewToStringInternal(string viewPath, object model, bool partial = false)
        {
            ViewEngineResult viewEngineResult = null;
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(Context, viewPath);
            else
                viewEngineResult = ViewEngines.Engines.FindView(Context, viewPath, null);

            if (viewEngineResult == null || viewEngineResult.View == null)
                throw new FileNotFoundException("View not foud");

            var view = viewEngineResult.View;
            Context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(Context, view, Context.Controller.ViewData, Context.Controller.TempData, sw);

                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }

        public static T CreateController<T>(RouteData routeData = null)
                    where T : Controller, new()
        {
            T controller = new T();

            HttpContextBase wrapper = null;

            if (HttpContext.Current != null)
                wrapper = new HttpContextWrapper(System.Web.HttpContext.Current);
            else
                throw new InvalidOperationException("Can't create Controller Context if no active HttpContext instance is available.");

            if (routeData == null)
                routeData = new RouteData();

            if (!routeData.Values.ContainsKey("controller") && !routeData.Values.ContainsKey("Controller"))
                routeData.Values.Add("controller", controller.GetType().Name.ToLower().Replace("controller", ""));

            controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);
            return controller;
        }

    }
}