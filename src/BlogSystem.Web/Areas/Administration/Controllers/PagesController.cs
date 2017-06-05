namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    using BlogSystem.Web.Infrastructure.Identity;
    using BlogSystem.Web.Infrastructure.Helpers.Url;
    using BlogSystem.Web.Infrastructure.XSS;
    using BlogSystem.Web.Areas.Administration.ViewModels.Pages;

    public class PagesController : AdministrationController
    {
        private readonly IDbRepository<Page> pagesData;
        private readonly IUrlGenerator urlGenerator;
        private readonly ICurrentUser currentUser;
        private readonly ISanitizer sanitizer;

        public PagesController(IDbRepository<Page> pagesData, IUrlGenerator urlGenerator, 
            ICurrentUser currentUser, ISanitizer sanitizer)
        {
            this.pagesData = pagesData;
            this.urlGenerator = urlGenerator;
            this.currentUser = currentUser;
            this.sanitizer = sanitizer;
        }

        public ActionResult Index()
        {
            var allPages = this.pagesData.All().OrderByDescending(p => p.CreatedOn);
            var pages = this.mapper.Map<PageViewModel>(allPages).ToList();

            var model = new IndexPagesViewModel
            {
                Pages = pages
            };

            return this.View(model);
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
                    AuthorId = this.currentUser.GetUser().Id,
                    Permalink = this.urlGenerator.GenerateUrl(model.Title)
                };

                this.pagesData.Add(page);
                this.pagesData.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var page = this.pagesData.Find(id);

            if (page == null)
            {
                return this.HttpNotFound();
            }

            var model = this.mapper.Map<PageViewModel>(page);

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
            var page = this.pagesData.Find(id);

            if (page == null)
            {
                return this.HttpNotFound();
            }

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