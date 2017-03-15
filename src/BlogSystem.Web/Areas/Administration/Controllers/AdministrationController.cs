namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Common;
    using System;
    using Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public abstract class AdministrationController : BaseController
    {
        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            this.ViewBag.Version = Assembly.GetExecutingAssembly().GetName().Version;
            this.ViewBag.IpAddress = requestContext.HttpContext.Request.UserHostAddress;

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}