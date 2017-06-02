namespace BlogSystem.Web.Infrastructure.Attributes
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PassSettingsToViewDataAttribute : ActionFilterAttribute, IActionFilter
    {
        private readonly IDbRepository<Setting> settingsData;

        public PassSettingsToViewDataAttribute()
        {
            this.settingsData = DependencyResolver.Current.GetService<IDbRepository<Setting>>();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var settings = this.settingsData.All().ToList();
            var viewData = filterContext.Controller.ViewData;

            foreach(var setting in settings)
            {
                viewData.Add(setting.Key, setting.Value);
            }
        }
    }
}