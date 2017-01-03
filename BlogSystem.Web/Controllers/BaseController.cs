namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Reflection;
    using System.Web.Routing;
    using System.Web.Mvc;
    using AutoMapper;
    using Infrastructure.Mapping;
    using Services.Cache;

    public abstract class BaseController : Controller
    {
        public ICacheService Cache { get; set; }

        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            /*
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
             * */

            this.ViewBag.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}