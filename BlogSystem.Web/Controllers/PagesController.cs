namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using ViewModels.Page;
    using Infrastructure.Mapping;

    public class PagesController : BaseController
    {
        public PagesController(IBlogSystemData data) 
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Page(string permalink)
        {
            var page = this.Data.Pages.All().Where(x => x.Permalink.ToLower().Trim() == permalink.ToLower().Trim()).To<PageViewModel>().FirstOrDefault();

            return View(page);
        }
    }
}