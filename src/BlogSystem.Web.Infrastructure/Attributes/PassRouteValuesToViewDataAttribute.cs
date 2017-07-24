namespace BlogSystem.Web.Infrastructure.Attributes
{
    using System;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Method)]
    public class PassRouteValuesToViewDataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var routes = filterContext.RouteData.Values;
            var viewData = filterContext.Controller.ViewData;

            foreach (var route in routes)
            {
                viewData.Add(route.Key, route.Value);
            }
        }
    }
}