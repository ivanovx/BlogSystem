using BlogSystem.Web.Infrastructure.Helpers.Url;

namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels.Pages;
    using Infrastructure.Helpers;
    using Infrastructure.Identity;
    using Data.Repositories;
    using Base;
    using BlogSystem.Services.Web.Mapping;

    using EntityModel = Data.Models.Page;
    using ViewModel = ViewModels.Pages.PageViewModel;

    public class PagesController : GenericAdministrationController<EntityModel, ViewModel>
    {
        private readonly IUrlGenerator urlGenerator;
        private readonly ICurrentUser currentUser;

        public PagesController(IDbRepository<EntityModel> dataRepository, IMappingService mappingService, IUrlGenerator urlGenerator, ICurrentUser currentUser)
            : base(dataRepository, mappingService)
        {
            this.urlGenerator = urlGenerator;
            this.currentUser = currentUser;
        }

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
                var model = this.mappingService.Map<ViewModel>(entity);

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