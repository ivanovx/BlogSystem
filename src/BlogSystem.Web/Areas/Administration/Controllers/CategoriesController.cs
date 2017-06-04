namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    using BlogSystem.Web.Areas.Administration.ViewModels.Categories;

    public class CategoriesController : AdministrationController
    {
        private readonly IDbRepository<Category> categoriesData;

        public CategoriesController(IDbRepository<Category> categoriesData)
        {
            this.categoriesData = categoriesData;
        }

        public ActionResult Index()
        {
            var allCategories = this.categoriesData.All().Where(c => !c.IsDeleted);
            var model = this.mapper.Map<CategoryViewModel>(allCategories).ToList();

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = model.Name
                };

                this.categoriesData.Add(category);
                this.categoriesData.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }
    }
}