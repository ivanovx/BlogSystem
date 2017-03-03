using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSystem.Data.Models;
using BlogSystem.Data.Repositories;
using BlogSystem.Services.Web.Mapping;
using BlogSystem.Web.Areas.Administration.Controllers.Base;
using BlogSystem.Web.Areas.Administration.ViewModels;

namespace BlogSystem.Web.Areas.Administration.Controllers
{
    public class CategoriesController : AdministrationController
    {
        private readonly IDbRepository<Category> categoryRepository;
        private readonly IMappingService mappingService;

        public CategoriesController(IDbRepository<Category> categoryRepository, IMappingService mappingService)
        {
            this.categoryRepository = categoryRepository;
            this.mappingService = mappingService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var categories = this.categoryRepository.All();
            var model = this.mappingService.Map<CategoryViewModel>(categories).ToList();

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = model.Name
                };

                this.categoryRepository.Add(category);
                this.categoryRepository.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }
    }
}