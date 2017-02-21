namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using Data.Repositories;
    using ViewModels.Nav;
    using Infrastructure.Extensions;
    using Infrastructure.Helpers;
    using Areas.Administration.Controllers.Base;
    using Infrastructure.Cache;

    public class NavController : BaseController
    {
        private readonly IDbRepository<Page> pagesRepository;
        private readonly ICacheService cacheService;

        public NavController(IDbRepository<Page> pagesRepository, ICacheService cacheService)
        {
            this.pagesRepository = pagesRepository;
            this.cacheService = cacheService;
        }

        [ChildActionOnly]
        public PartialViewResult Menu()
        { 
            var model = this.cacheService.Get("Menu", () =>
                this.pagesRepository
                    .All()
                    .Where(p => p.VisibleInMenu)
                    .To<MenuItemViewModel>()
                    .ToList(),
                600);

            return this.PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult AdminMenu()
        {
            var controllers =
                ReflectionHelper
                    .GetSubClasses<AdministrationController>()
                    .Select(c => c.Name.Replace("Controller", string.Empty));

            return this.PartialView(controllers);
        }
    }
}