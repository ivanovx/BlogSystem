using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSystem.Web.Areas.Administration.Controllers.Base;

namespace BlogSystem.Web.Areas.Administration.Controllers
{
    public class TinyMceUpload : AdministrationController
    {
        // GET: Administration/TinyMceUpload
        public ActionResult Index()
        {
            return Content("");
        }
    }
}