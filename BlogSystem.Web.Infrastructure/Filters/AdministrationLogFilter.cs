namespace BlogSystem.Web.Infrastructure.Filters
{
    using System;
    using System.Web.Mvc;
    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;
    using BlogSystem.Web.Infrastructure.Attributes;
    using Microsoft.AspNet.Identity;

    public class AdministrationLogFilter : IActionFilter
    {
        private readonly IDbRepository<AdministrationLog> administrationLogs;

        public AdministrationLogFilter(IDbRepository<AdministrationLog> administrationLogs)
        {
            if (administrationLogs == null)
            {
                throw new ArgumentNullException(nameof(administrationLogs));
            }

            this.administrationLogs = administrationLogs;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var administrationLog = new AdministrationLog
            {
                IpAddress = filterContext.HttpContext.Request.UserHostAddress,
                Url = filterContext.HttpContext.Request.RawUrl,
                UserId = filterContext.HttpContext.User.Identity.GetUserId(),
                RequestType = filterContext.HttpContext.Request.RequestType,
                PostParams = filterContext.HttpContext.Request.Unvalidated.Form.ToString()
            };

            this.administrationLogs.Add(administrationLog);
            this.administrationLogs.SaveChanges();
        }
    }
}
