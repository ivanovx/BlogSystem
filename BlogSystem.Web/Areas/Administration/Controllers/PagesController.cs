using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSystem.Data.Models;
using BlogSystem.Data.UnitOfWork;
using BlogSystem.Web.Helpers;

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
                .ToList();

            return View(pages);
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
        public ActionResult Create(Page page)
        {
            if (ModelState.IsValid)
            {
                this.Data.Pages.Add(new Page
                {
                    Title = page.Title,
                    Content = page.Content,
                    CreatedOn = DateTime.Now,
                    Author = this.UserProfile,
                    AuthorId = this.UserProfile.Id,
                    Permalink = new UrlGenerator().GenerateUrl(page.Title)
                });
                this.Data.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
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