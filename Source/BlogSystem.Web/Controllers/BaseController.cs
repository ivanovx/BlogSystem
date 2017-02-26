namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using System.Web.Routing;
    using System.Web.Mvc;
    using Data.Models;
    using Data.Repositories;
    using Infrastructure;

    public abstract class BaseController : Controller
    {
        private readonly IDbRepository<Setting> settings;

        protected BaseController()
        {
            this.settings = DependencyResolver.Current.GetService<IDbRepository<Setting>>();
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            Func<IDictionary<string, string>> getSettings = delegate()
            {
                return this.settings.All().ToDictionary(s => s.Key, s => s.Value);
            };

            this.ViewBag.Settings = new SettingsManager(getSettings);
            this.ViewBag.Version = Assembly.GetExecutingAssembly().GetName().Version;
            this.ViewBag.IpAddress = requestContext.HttpContext.Request.UserHostAddress;

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}