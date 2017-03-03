using BlogSystem.Data.Models;

namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels.Posts;
    using Infrastructure.Identity;
    using Data.Repositories;
    using BlogSystem.Services.Web.Mapping;
    using Common;
    using Base;

    using EntityModel = Data.Models.Post;
    using ViewModel = ViewModels.Posts.PostViewModel;

    public class PostsController : GenericAdministrationController<EntityModel, ViewModel>
    {
        private readonly IDbRepository<Category> categoryRepository;
        private readonly ICurrentUser currentUser;

        public PostsController(IDbRepository<EntityModel> dataRepository, IDbRepository<Category> categoryRepository, IMappingService mappingService, ICurrentUser currentUser) 
            : base(dataRepository, mappingService)
        {
            this.categoryRepository = categoryRepository;
            this.currentUser = currentUser;
        }

        public ActionResult Index(int page = 1, int perPage = GlobalConstants.DefaultPageSize)
        {
            int pagesCount = (int) Math.Ceiling(this.dataRepository.All().Count() / (decimal) perPage);

            var posts = this.GetAll().Skip(perPage * (page - 1)).Take(perPage).ToList();

            var model = new IndexPostsPageViewModel
            {
                Posts = posts,
                CurrentPage = page,
                PagesCount = pagesCount
            };

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            this.ViewBag.Categories = this.categoryRepository.All().OrderBy(c => c.Name).ToDictionary(c => c.Id, c => c.Name);

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModel model)
        {
            model.AuthorId = this.currentUser.Get().Id;

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
            var model = this.dataRepository.Find(id);

            return this.View(model);
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