namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using Data.Repositories;

    public class CategoriesController : AdministrationController
    {
        private readonly IDbRepository<Category> dbRepository;

        public CategoriesController(IDbRepository<Category> dbRepository)
        {
            this.dbRepository = dbRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var categories = this.dbRepository.All().ToList();

            return this.View(categories);
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