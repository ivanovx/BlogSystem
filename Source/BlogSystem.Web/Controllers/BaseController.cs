namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using System.Web.Routing;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.Models;
    using Data.Repositories;
    using Infrastructure;
    using Infrastructure.Mapping;
    using Infrastructure.Cache;

    public abstract class BaseController : Controller
    {
        public ICacheService Cache { get; set; }

        public IDbRepository<Setting> Settings { get; set; }

        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.MapperConfiguration.CreateMapper();
            }
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            Func<IDictionary<string, string>> getSettings = delegate()
            {
                return this.Settings.All().ToDictionary(s => s.Key, s => s.Value);
            };

            this.ViewBag.Settings = new SettingsManager(getSettings);
            this.ViewBag.Version = Assembly.GetExecutingAssembly().GetName().Version;

            this.ViewBag.ipAddress = requestContext.HttpContext.Request.UserHostAddress;

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}