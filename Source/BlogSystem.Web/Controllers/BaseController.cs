using BlogSystem.Web.Infrastructure.Helpers;

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

    public abstract class BaseController : Controller
    {
        public IDbRepository<Setting> Settings { get; set; }

        private readonly IDbRepository<Setting> settings;

        public BaseController()
        {
            this.settings = DependencyResolver.Current.GetService<IDbRepository<Setting>>();
        }

        protected IMapper Mapper => AutoMapperConfig.MapperConfiguration.CreateMapper();

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            Func<IDictionary<string, string>> getSettings = delegate()
            {
                return this.settings.All().ToDictionary(s => s.Key, s => s.Value);
            };

            this.ViewBag.Settings = new SettingsManager(getSettings);
            this.ViewBag.Version = Assembly.GetExecutingAssembly().GetName().Version;
            this.ViewBag.IpAddress = RequestHelpers.GetClientIpAddress(requestContext);

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}