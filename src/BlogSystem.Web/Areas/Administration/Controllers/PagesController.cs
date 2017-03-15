namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using Data.Repositories;
    using ViewModels.Pages;
    using Infrastructure;
    using Infrastructure.Identity;
    using Infrastructure.Helpers.Url;
    using Services.Web.Mapping;
    
    public class PagesController : AdministrationController
    {
        private readonly IDbRepository<Page> pagesData;
        private readonly IMappingService mappingService;
        private readonly IUrlGenerator urlGenerator;
        private readonly ICurrentUser currentUser;
        private readonly ISanitizer sanitizer;

        public PagesController(IDbRepository<Page> pagesData, IMappingService mappingService, 
            IUrlGenerator urlGenerator, ICurrentUser currentUser, ISanitizer sanitizer)
        {
            this.pagesData = pagesData;
            this.mappingService = mappingService;
            this.urlGenerator = urlGenerator;
            this.currentUser = currentUser;
            this.sanitizer = sanitizer;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var pages = this.pagesData.All().OrderByDescending(p => p.CreatedOn);
            var model = this.mappingService.Map<PageViewModel>(pages).ToList();

            var viewModel = new IndexPagesViewModel
            {
                Pages = model
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PageViewModel model)
        {
            if(model != null && this.ModelState.IsValid)
            {
                var page = new Page
                {
                    Title = model.Title,
                    Content =  this.sanitizer.Sanitize(model.Content),
                    VisibleInMenu = model.VisibleInMenu,
                    AuthorId = this.currentUser.GetUser.Id,
                    Permalink = this.urlGenerator.GenerateUrl(model.Title)
                };

                this.pagesData.Add(page);
                this.pagesData.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.HttpNotFound();
            }

            var page = this.pagesData.Find(id);
            var model = this.mappingService.Map<PageViewModel>(page);

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PageViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var page = this.pagesData.Find(model.Id);

                page.Title = model.Title;
                page.Content = this.sanitizer.Sanitize(model.Content);
                page.VisibleInMenu = model.VisibleInMenu;
                page.Permalink = this.urlGenerator.GenerateUrl(model.Title);

                this.pagesData.Update(page);
                this.pagesData.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return this.HttpNotFound();
            }

            var page = this.pagesData.Find(id);

            return this.View(page);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (this.ModelState.IsValid)
            {
                this.pagesData.Remove(id);
                this.pagesData.SaveChanges();
            }

            return this.RedirectToAction("Index");
        }
    }
}