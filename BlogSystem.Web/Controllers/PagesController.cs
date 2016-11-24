using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSystem.Data.UnitOfWork;
using BlogSystem.Web.ViewModels.Page;
using BlogSystem.Web.Infrastructure.Mapping;

namespace BlogSystem.Web.Controllers
{
    public class PagesController : BaseController
    {
        public PagesController(IBlogSystemData data) 
            : base(data)
        {
        }

        // GET: Pages
        [HttpGet]
        public ActionResult Page(int id)
        {
            var page = this.Data.Pages.All()
                .Where(x => x.Id == id)
                .To<PageViewModel>()
                .FirstOrDefault();

            return View(page);
        }
    }
}