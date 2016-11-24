using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSystem.Data.Models;
using BlogSystem.Data.UnitOfWork;
using BlogSystem.Web.Areas.Administration.InputModels.Page;
using BlogSystem.Web.Areas.Administration.ViewModels.Page;
using BlogSystem.Web.Helpers;
using BlogSystem.Web.Infrastructure.Mapping;

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
            var pages = this.Data.Pages
                .All()
                .OrderByDescending(p => p.CreatedOn)
                .To<PageViewModel>();

            var model = new IndexPagesViewModel
            {
                Pages = pages.ToList()
            };

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePageInputModel pageInputModel)
        {
            if (ModelState.IsValid)
            {
                var page = new Page
                {
                    Title = pageInputModel.Title,
                    Content = pageInputModel.Content,
                    Author = this.UserProfile,
                    AuthorId = this.UserProfile.Id,
                    Permalink = new UrlGenerator().GenerateUrl(pageInputModel.Title)
                };

                this.Data.Pages.Add(page);
                this.Data.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(pageInputModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            return View();
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Page page)
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}