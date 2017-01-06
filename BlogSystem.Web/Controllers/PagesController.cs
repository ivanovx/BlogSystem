using BlogSystem.Data.Models;
using BlogSystem.Data.Repositories;

namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using ViewModels.Page;
    using Infrastructure.Extensions;

    public class PagesController : BaseController
    {
        private readonly IDbRepository<Page> pagesRepository;

        public PagesController(IDbRepository<Page> pagesRepository)
        {
            this.pagesRepository = pagesRepository;
        }

        public ActionResult Page(string permalink)
        {
            var page = this.pagesRepository
                .All()
                .Where(x => x.Permalink.ToLower().Trim() == permalink.ToLower().Trim())
                .To<PageViewModel>()
                .FirstOrDefault();

            return View(page);
        }
    }
}