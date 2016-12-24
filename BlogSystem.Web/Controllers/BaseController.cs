namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Reflection;
    using System.Web.Routing;
    using System.Web.Mvc;
    using AutoMapper;
    using Infrastructure.Mapping;

    public abstract class BaseController : Controller
    {
        private readonly string version;

        protected BaseController()
        {
            this.version = Assembly.GetAssembly(typeof(Startup)).GetName().Version.ToString();
        }

        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            this.ViewBag.Version = this.version;

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}