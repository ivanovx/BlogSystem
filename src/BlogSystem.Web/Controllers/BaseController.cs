namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Web.Routing;
    using System.Web.Mvc;
    using Infrastructure;

    public abstract class BaseController : Controller
    {
        private readonly ISettingsManager settingsManager;

        protected BaseController()
        {
            this.settingsManager = DependencyResolver.Current.GetService<ISettingsManager>();
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, 
            AsyncCallback callback, object state)
        {
            this.ViewBag.Settings = settingsManager;

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}