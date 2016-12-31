namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Data.Models;
    using Data.UnitOfWork;
    using InputModels.Page;
    using ViewModels.Page;
    using Infrastructure.Mapping;
    using Infrastructure.Helpers;
    using Infrastructure.Identity;

    public class PagesController : AdministrationController
    {
        private readonly IBlogSystemData data;
        private readonly IUrlGenerator urlGenerator;
        private readonly ICurrentUser currentUser;

        public PagesController(IBlogSystemData data, IUrlGenerator urlGenerator, ICurrentUser currentUser)
        {
            this.data = data;
            this.urlGenerator = urlGenerator;
            this.currentUser = currentUser;
        }

        // GET: Administration/Pages
        public ActionResult Index()
        {
            var pages = this.data.Pages
                .All()
                .OrderByDescending(p => p.CreatedOn)
                .To<PageViewModel>()
                .ToList();

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
        public ActionResult Create(CreatePageInputModel pageInputModel)
        {
            if (ModelState.IsValid)
            {
                var page = new Page
                {
                    Title = pageInputModel.Title,
                    Content = pageInputModel.Content,
                    Permalink = this.urlGenerator.GenerateUrl(pageInputModel.Title),
                    Author = this.currentUser.Get(),
                    AuthorId = this.currentUser.Get().Id,
                    VisibleInMenu = pageInputModel.VisibleInMenu
                };

                this.data.Pages.Add(page);
                this.data.SaveChanges();

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

            var page = this.data.Pages.Find(id);

            if (page == null)
            {
                return this.HttpNotFound();
            }

            var model = new EditPageInputModel
            {
                Id = page.Id,
                Title = page.Title,
                Content = page.Content,
                CreatedOn = page.CreatedOn,
                AuthorId = page.AuthorId,
                VisibleInMenu = page.VisibleInMenu
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditPageInputModel pageInputModel)
        {
            if (this.ModelState.IsValid)
            {
                var page = this.data.Pages.Find(pageInputModel.Id);

                page.Id = pageInputModel.Id;
                page.Title = pageInputModel.Title;
                page.Content = pageInputModel.Content;
                page.AuthorId = pageInputModel.AuthorId;
                page.CreatedOn = pageInputModel.CreatedOn;
                page.VisibleInMenu = pageInputModel.VisibleInMenu;

                this.data.Pages.Update(page);
                this.data.SaveChanges();

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

            var page = this.data.Pages.Find(id);

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
            var page = this.data.Pages.Find(id);

            this.data.Pages.Remove(page);
            this.data.SaveChanges();

            return this.RedirectToAction("Index");
        }
    }
}