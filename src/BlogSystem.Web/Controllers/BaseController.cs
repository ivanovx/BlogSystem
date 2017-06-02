namespace BlogSystem.Web.Controllers
{
    using System.Web.Mvc;

    using BlogSystem.Services.Web.Caching;
    using BlogSystem.Services.Web.Mapping;
    using BlogSystem.Web.Infrastructure.Attributes;

    [HandleError]
    [PassSettingsToViewData]
    public abstract class BaseController : Controller
    {
        protected readonly ICacheService cache;
        protected readonly IMappingService mapping;
        
        protected BaseController()
        {
            this.cache = DependencyResolver.Current.GetService<ICacheService>();
            this.mapping = DependencyResolver.Current.GetService<IMappingService>();
        }
    }
}