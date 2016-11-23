using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSystem.Data.UnitOfWork;

namespace BlogSystem.Web.Areas.Administration.Controllers
{
    public class PagesController : AdministrationController
    {
        public PagesController(IBlogSystemData data) 
            : base(data)
        {
        }

        // GET: Administration/Pages
        public ActionResult Index()
        {
            var pages = this.Data.Pages.All().ToList();

            return View(pages);
        }
    }
}