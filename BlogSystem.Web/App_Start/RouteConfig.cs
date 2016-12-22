namespace BlogSystem.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Page",
                url: "Pages/{permalink}",
                defaults: new
                {
                    controller = "Pages",
                    action = "Page"
                },
                namespaces: new[]
                {
                    "BlogSystem.Web.Controllers"
                });

            routes.MapRoute(
               name: "Post",
               url: "Posts/{id}",
               defaults: new
               {
                   controller = "Posts",
                   action = "Details",
                   id = UrlParameter.Optional
               },
               namespaces: new[]
               {
                   "BlogSystem.Web.Controllers"
               });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                namespaces: new[]
                {
                    "BlogSystem.Web.Controllers"
                });
        }
    }
}