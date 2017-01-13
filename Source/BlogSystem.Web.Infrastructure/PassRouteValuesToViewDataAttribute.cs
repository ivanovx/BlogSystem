using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Web.Infrastructure
{
    using System.Web.Mvc;

    public class PassRouteValuesToViewDataAttribute : ActionFilterAttribute, IActionFilter
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
