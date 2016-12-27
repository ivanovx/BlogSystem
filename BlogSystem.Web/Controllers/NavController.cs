namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using Infrastructure.Mapping;
    using ViewModels.Nav;

    public class NavController : BaseController
    {
        private readonly IBlogSystemData data;

        public NavController(IBlogSystemData data)
        {
            this.data = data;
        }

        [ChildActionOnly]
        [OutputCache(Duration = 6 * 10 * 60)]
        public PartialViewResult Menu()
        { 
            var model = this.Cache.Get("Menu", () =>
                this.data.Pages
                    .All()
                    .To<MenuItemViewModel>()
                    .ToList(),
                600);

            return this.PartialView(model);
        }
    }
}