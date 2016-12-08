namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using BlogSystem.Data.UnitOfWork;
    using BlogSystem.Web.Infrastructure.Mapping;
    using BlogSystem.Web.ViewModels;

    public class NavController : BaseController
    {
        public NavController(IBlogSystemData data) 
            : base(data)
        {
        }

        [ChildActionOnly]
        public PartialViewResult Menu()
        {
            var menu = this.Data.Pages
                .All()
                .To<MenuItemViewModel>()
                .ToList();

            return this.PartialView("_Menu", menu);
        }
    }
}