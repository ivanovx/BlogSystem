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
        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            this.ViewBag.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}