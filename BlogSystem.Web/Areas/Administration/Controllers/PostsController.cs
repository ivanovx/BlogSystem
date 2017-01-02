namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels.Post;
    using Infrastructure.Identity;
    using Data.Repositories;
    using Common;
    using Base;

    using EntityModel = Data.Models.Post;
    using ViewModel = ViewModels.Post.PostViewModel;

    public class PostsController : GenericAdministrationController<EntityModel, ViewModel>
    {
        private readonly IDbRepository<EntityModel> dataRepository;
        private readonly ICurrentUser currentUser;

        public PostsController(IDbRepository<EntityModel> dataRepository, ICurrentUser currentUser) 
            : base(dataRepository)
        {
            this.dataRepository = dataRepository;
            this.currentUser = currentUser;
        }

        // GET: Administration/Posts
        public ActionResult Index(int page = 1, int perPage = GlobalConstants.DefaultPageSize)
        {
            int pagesCount = (int) Math.Ceiling(this.dataRepository.All().Count() / (decimal) perPage);

            var posts = this.GetAll()
                .Skip(perPage * (page - 1))
                .Take(perPage)
                .ToList();

            var model = new IndexPostsPageViewModel
            {
                Posts = posts,
                CurrentPage = page,
                PagesCount = pagesCount
            };

            return this.View(model);
        }

        // GET: Administration/Posts/Create
        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Posts/Create
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

        // GET: Administration/Posts/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var entity = this.dataRepository.Find(id);

            if (entity != null)
            {
                var model = this.Mapper.Map<ViewModel>(entity);

                model.AuthorId = this.currentUser.Get().Id;

                return this.View(model);
            }

            return this.RedirectToAction("Index");
        }

        // POST: Administration/Posts/Edit/5
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

        // GET: Administration/Posts/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var model = this.dataRepository.Find(id);

            return this.View(model);
        }

        // POST: Administration/Posts/Delete/5
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