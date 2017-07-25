namespace BlogSystem.Web.Controllers
{
    using System.Web.Mvc;

    using BlogSystem.Web.Infrastructure.Identity;
    using BlogSystem.Web.Infrastructure.Attributes;

    using BlogSystem.Web.Services.Mapping;
    using BlogSystem.Web.Services.Caching;

    [PassSettingsToViewData]
    public abstract class BaseController : Controller
    {
        public ICurrentUser CurrentUser { get; set; }

        public IMappingService Mapper { get; set; }

        public ICacheService Cache { get; set; }
    }
}