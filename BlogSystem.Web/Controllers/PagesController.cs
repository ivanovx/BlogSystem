namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using BlogSystem.Data.UnitOfWork;
    using BlogSystem.Web.ViewModels.Page;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class PagesController : BaseController
    {
        public PagesController(IBlogSystemData data) 
            : base(data)
        {
        }

        // GET: Pages
        [HttpGet]
        public ActionResult Page(string permalink)
        {
            var page = this.Data.Pages.All()
                .Where(p => p.Permalink == permalink)
                .To<PageViewModel>()
                .FirstOrDefault();

            return View(page);
        }
    }
}