namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using ViewModels.Page;
    using Infrastructure.Mapping;

    public class PagesController : BaseController
    {
        private readonly IBlogSystemData data;

        public PagesController(IBlogSystemData data)
        {
            this.data = data;
        }

        public ActionResult Page(string permalink)
        {
            var page = this.data.Pages
                .All()
                .Where(x => x.Permalink.ToLower().Trim() == permalink.ToLower().Trim())
                .To<PageViewModel>()
                .FirstOrDefault();

            return View(page);
        }
    }
}