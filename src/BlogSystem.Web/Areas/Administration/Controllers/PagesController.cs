namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Net;
    using System.Linq;
    using System.Web.Mvc;

    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    using BlogSystem.Web.Infrastructure.Helpers.Url;
    using BlogSystem.Web.Infrastructure.XSS;

    using BlogSystem.Web.Areas.Administration.ViewModels.Pages;

    public class PagesController : AdministrationController
    {
        private readonly IDbRepository<Page> pagesData;
        private readonly IUrlGenerator urlGenerator;
        private readonly ISanitizer sanitizer;

        public PagesController(IDbRepository<Page> pagesData, IUrlGenerator urlGenerator, ISanitizer sanitizer)
        {
            this.pagesData = pagesData;
            this.urlGenerator = urlGenerator;
            this.sanitizer = sanitizer;
        }

        public ActionResult Index()
        {
            var allPages = this.pagesData.All().OrderByDescending(p => p.CreatedOn);
            var pages = this.Mapper.Map<PageViewModel>(allPages).ToList();

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
                    AuthorId = this.CurrentUser.GetUser().Id,
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var page = this.pagesData.Find(id);

            if (page == null)
            {
                return this.HttpNotFound();
            }

            var model = this.Mapper.Map<PageViewModel>(page);

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
                page.AuthorId = this.CurrentUser.GetUser().Id;

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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

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