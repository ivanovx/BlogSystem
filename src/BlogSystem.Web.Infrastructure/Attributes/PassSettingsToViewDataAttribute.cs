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
        public IDbRepository<Setting> Settings { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var settings = this.Settings.All().ToList();
            var viewData = filterContext.Controller.ViewData;

            foreach(var setting in settings)
            {
                viewData.Add(setting.Key, setting.Value);
            }
        }
    }
}