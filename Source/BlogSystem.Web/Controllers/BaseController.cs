namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Reflection;
    using System.Web.Routing;
    using System.Web.Mvc;
    using Infrastructure;

    public abstract class BaseController : Controller
    {
        private readonly ISettingsManager settingsManager;

        public BaseController()
        {
            this.settingsManager = DependencyResolver.Current.GetService<ISettingsManager>();
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            this.ViewBag.Settings = settingsManager.GetSettings();
            this.ViewBag.Version = Assembly.GetExecutingAssembly().GetName().Version;
            this.ViewBag.IpAddress = requestContext.HttpContext.Request.UserHostAddress;

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}