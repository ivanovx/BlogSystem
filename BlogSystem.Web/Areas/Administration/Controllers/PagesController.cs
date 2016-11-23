using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSystem.Web.Areas.Administration.Controllers
{
    public class PagesController : Controller
    {
        // GET: Administration/Pages
        public ActionResult Index()
        {
            return View();
        }
    }
}