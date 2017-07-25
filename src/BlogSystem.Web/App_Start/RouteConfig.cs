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
               name: "Posts",
               url: "Posts/{slug}/{id}",
               defaults: new
               {
                   controller = "Blog",
                   action = "Post"
               },
               namespaces: new[]
               {
                   "BlogSystem.Web.Controllers"
               });

            routes.MapRoute(
                name: "Pages",
                url: "Pages/{slug}/{id}",
                defaults: new
                {
                    controller = "Blog",
                    action = "Page"
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