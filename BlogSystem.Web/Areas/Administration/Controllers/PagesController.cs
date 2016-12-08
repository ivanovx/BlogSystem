namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using BlogSystem.Data.Models;
    using BlogSystem.Data.UnitOfWork;
    using BlogSystem.Web.Areas.Administration.InputModels.Page;
    using BlogSystem.Web.Areas.Administration.ViewModels.Page;
    using BlogSystem.Web.Infrastructure.Mapping;
    using Infrastructure.Helpers;

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

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
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
                    Permalink = new UrlGenerator().GenerateUrl(pageInputModel.Title),
                    Author = this.UserProfile,
                    AuthorId = this.UserProfile.Id,
                };

                this.Data.Pages.Add(page);
                this.Data.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(pageInputModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var page = this.Data.Pages.Find(id);

            if (page == null)
            {
                return this.HttpNotFound();
            }

            var model = new EditPageInputModel
            {
                Id = page.Id,
                Content = page.Content,
                CreatedOn = page.CreatedOn,
                Title = page.Title,
                AuthorId = page.AuthorId
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditPageInputModel pageInputModel)
        {
            if (this.ModelState.IsValid)
            {
                var page = this.Data.Pages.Find(pageInputModel.Id);

                page.Id = pageInputModel.Id;
                page.Title = pageInputModel.Title;
                page.Content = pageInputModel.Content;
                page.AuthorId = pageInputModel.AuthorId;
                page.CreatedOn = pageInputModel.CreatedOn;

                this.Data.Pages.Update(page);
                this.Data.SaveChanges();

                return this.RedirectToAction("Index");
           }

            return this.View(pageInputModel);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var page = this.Data.Pages.Find(id);

            if (page == null)
            {
                return this.HttpNotFound();
            }

            return this.View(page);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var page = this.Data.Pages.Find(id);

            this.Data.Pages.Remove(page);
            this.Data.SaveChanges();

            return this.RedirectToAction("Index");
        }
    }
}