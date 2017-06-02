namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using BlogSystem.Services.Data.Pages;
    using BlogSystem.Web.ViewModels.Common;

    public class NavController : BaseController
    {
        private readonly IPagesDataService pagesData;

        public NavController(IPagesDataService pagesData)
        {
            this.pagesData = pagesData;
        }

        [ChildActionOnly]
        public PartialViewResult Menu()
        {
            var pages = this.pagesData.GetAllPagesForMenu();
            var model = this.cache.Get("Menu", () => {
                return this.mapping.Map<MenuItemViewModel>(pages).ToList();
            }, 600);

            return this.PartialView(model);
        }
    }
}