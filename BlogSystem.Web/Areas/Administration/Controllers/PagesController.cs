using BlogSystem.Data.Repositories;
using BlogSystem.Web.Areas.Administration.Controllers.Base;
using BlogSystem.Web.Areas.Administration.ViewModels.Post;

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

    using EntityModel = BlogSystem.Data.Models.Page;
    using ViewModel = BlogSystem.Web.Areas.Administration.ViewModels.Page.PageViewModel;

    public class PagesController : GenericAdministrationController<EntityModel, ViewModel>
    {
        private readonly IDbRepository<EntityModel> dataRepository;
       // private readonly IBlogSystemData data;
        private readonly IUrlGenerator urlGenerator;
        private readonly ICurrentUser currentUser;

        /*public PagesController(IBlogSystemData data, IUrlGenerator urlGenerator, ICurrentUser currentUser)
        {
            this.data = data;
            this.urlGenerator = urlGenerator;
            this.currentUser = currentUser;
        }*/

        public PagesController(IDbRepository<EntityModel> dataRepository, IUrlGenerator urlGenerator, ICurrentUser currentUser) : base(dataRepository)
        {
            this.dataRepository = dataRepository;
            this.urlGenerator = urlGenerator;
            this.currentUser = currentUser;
        }

        // GET: Administration/Pages
        public ActionResult Index()
        {
            var pages = this.GetAll().ToList();

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
        public ActionResult Create(ViewModel model)
        {
            /*if (ModelState.IsValid)
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

            return this.View(pageInputModel);*/

            model.AuthorId = this.currentUser.Get().Id;
            model.Permalink = this.urlGenerator.GenerateUrl(model.Title);

            var entity = this.CreateEntity(model);

            if (entity != null)
            {
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var entity = this.dataRepository.Find(id);

            if (entity != null)
            {
                var model = this.Mapper.Map<ViewModel>(entity);
                model.AuthorId = this.currentUser.Get().Id;
                model.Permalink = this.urlGenerator.GenerateUrl(entity.Title);

                return this.View(model);
            }

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModel model)
        {
            var entity = this.FindAndUpdateEntity(model.Id, model);

            if (entity != null)
            {
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var page = this.dataRepository.Find(id);

            return this.View(page);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.DestroyEntity(id);

            return this.RedirectToAction("Index");
        }
    }
}