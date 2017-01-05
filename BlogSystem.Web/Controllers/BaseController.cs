using System.Collections.Generic;
using BlogSystem.Data.Models;
using BlogSystem.Data.Repositories;

namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Reflection;
    using System.Web.Routing;
    using System.Web.Mvc;
    using AutoMapper;
    using Infrastructure.Mapping;
    using Services.Cache;
    using System.Linq;

    public abstract class BaseController : Controller
    {
        public ICacheService Cache { get; set; }
    
        public IDbRepository<Setting> Settings { get; set; }

        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }

        private IDictionary<string, string> GetSettings()
        {
            return this.Settings.All().ToDictionary(s => s.Id, s => s.Value);
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            this.ViewBag.Settings = this.GetSettings();
            this.ViewBag.Version = Assembly.GetExecutingAssembly().GetName().Version;

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}