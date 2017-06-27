namespace BlogSystem.Web
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Reflection;

    using BlogSystem.Web.Infrastructure.Mapping;

    public class MvcApplication : HttpApplication
    {   
        protected void Application_Start()
        {
            DataConfig.ConfigureDatabase();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ViewEngineConfig.RegisterViewEngines(ViewEngines.Engines);

            AutofacConfig.RegisterAutofac();
            AutoMapperConfig.RegisterMappings(Assembly.GetExecutingAssembly());

            MvcHandler.DisableMvcResponseHeader = true;
        }
    }
}