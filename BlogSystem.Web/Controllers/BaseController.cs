namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Microsoft.AspNet.Identity;
    using BlogSystem.Data.Models;
    using BlogSystem.Data.UnitOfWork;

    public abstract class BaseController : Controller
    {
        protected BaseController(IBlogSystemData data)
        {
            this.Data = data;
        }

        protected BaseController(IBlogSystemData data, ApplicationUser userProfile) 
            : this(data)
        {
            this.UserProfile = userProfile;
        }

        protected IBlogSystemData Data { get; }

        protected ApplicationUser UserProfile { get; private set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                string username = requestContext
                    .HttpContext
                    .User
                    .Identity
                    .GetUserName();

                ApplicationUser user = this.Data.Users
                    .All()
                    .FirstOrDefault(u => u.UserName == username);

                this.UserProfile = user;
                this.ViewBag.UserProfile = user;
            }

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}