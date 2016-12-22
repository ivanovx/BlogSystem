namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Data.Models;
    using Data.UnitOfWork;
    using Infrastructure.Identity;
    using Microsoft.AspNet.Identity;
    using AutoMapper;
    using BlogSystem.Web.Infrastructure.Mapping;

    public abstract class BaseController : Controller
    {
        /*protected BaseController(IBlogSystemData data)
        {
            this.Data = data;
        }
 
        protected BaseController(IBlogSystemData data, ICurrentUser userProfile) 
            : this(data)
        {
            this.UserProfile = userProfile.Get();
        }*/

        /*  protected IBlogSystemData Data { get; }

          protected ApplicationUser UserProfile { get; private set; }*/

        /*protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //var username = requestContext.HttpContext.User.Identity.GetUserName();
                // var username = this.UserProfile.UserName;
                //var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);

                //this.UserProfile = user;
                //this.ViewBag.UserProfile = user;
            }

            return base.BeginExecute(requestContext, callback, state);
        }*/

        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }
    }
}