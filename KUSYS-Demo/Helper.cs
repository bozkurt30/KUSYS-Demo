using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text;

namespace KUSYS_Demo
{
    public class Helper
    {
        public static string RenderRazorViewToString(Controller controller, string viewName, object model = null)
        {
            controller.ViewData.Model = model;
            using (var stWrite = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewresult = viewEngine.FindView(controller.ControllerContext, viewName, false);
                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewresult.View,
                    controller.ViewData,
                    controller.TempData,
                    stWrite,
                    new HtmlHelperOptions()
                    );
                viewresult.View.RenderAsync(viewContext);
                return stWrite.GetStringBuilder().ToString();
            }
        }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Helper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// OnActionExecuting
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var session = _httpContextAccessor.HttpContext.Session.Get("username");
            if (session == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", controller = "Login" }));
            }
        }

        /// <summary>
        /// OnActionExecuted
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    //public class NoDirectAccessAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        if (filterContext.HttpContext.Request.GetTypedHeaders().Referer == null || filterContext.HttpContext.Request.GetTypedHeaders().Host.Host.ToString() != filterContext.HttpContext.Request.GetTypedHeaders().Referer.Host.ToString())
    //        {
    //            filterContext.HttpContext.Response.Redirect("/");
    //        }
    //    }
    //}
}
