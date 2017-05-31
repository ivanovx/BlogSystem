using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSystem.Data.Models;
using BlogSystem.Data.Repositories;

namespace BlogSystem.Web.Areas.Administration.Controllers
{
    public class CategoriesController : AdministrationController
    {
        private readonly IDbRepository<Category> dbRepository;

        public CategoriesController(IDbRepository<Category> dbRepository)
        {
            this.dbRepository = dbRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category model)
        {
            if (model != null && ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = model.Name
                };

                this.dbRepository.Add(category);
                this.dbRepository.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }
    }
}