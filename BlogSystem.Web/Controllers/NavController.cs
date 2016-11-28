using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSystem.Data.UnitOfWork;
using BlogSystem.Web.Infrastructure.Mapping;
using BlogSystem.Web.ViewModels;

namespace BlogSystem.Web.Controllers
{
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