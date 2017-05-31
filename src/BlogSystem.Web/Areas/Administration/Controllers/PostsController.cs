namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;   
    using Common;
    using Data.Models;
    using Data.Repositories;
    using Services.Web.Mapping;
    using Web.Infrastructure.XSS;
    using Infrastructure.Identity;
    using ViewModels.Posts;    

    public class PostsController : AdministrationController
    {
        private readonly IDbRepository<Post> postsData;
        private readonly IDbRepository<Category> categoriesData;
        private readonly IMappingService mappingService;
        private readonly ICurrentUser currentUser;
        private readonly ISanitizer sanitizer;

        public PostsController(IDbRepository<Post> postsData, IDbRepository<Category> categoriesData,
            IMappingService mappingService, ICurrentUser currentUser, ISanitizer sanitizer) 
        {
            this.postsData = postsData;
            this.categoriesData = categoriesData;
            this.mappingService = mappingService;
            this.currentUser = currentUser;
            this.sanitizer = sanitizer;
        }

        [HttpGet]
        public ActionResult Index(int page = 1, int perPage = GlobalConstants.DefaultPageSize)
        {
            int pagesCount = (int) Math.Ceiling(this.postsData.All().Count() / (decimal) perPage);

            var postsPage = this.postsData
                .All()
                .OrderByDescending(p => p.CreatedOn)
                .Skip(perPage * (page - 1))
                .Take(perPage);

            var posts = this.mappingService.Map<PostViewModel>(postsPage).ToList();

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
            this.ViewBag.Categories = this.categoriesData.All().ToList(); 

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var post = new Post
                {
                    Title = model.Title,
                    CategoryId = model.CategoryId,
                    Content = this.sanitizer.Sanitize(model.Content),
                    AuthorId = this.currentUser.GetUser().Id
                };

                this.postsData.Add(post);
                this.postsData.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            this.ViewBag.Categories = this.categoriesData.All().ToList();

            var post = this.postsData.Find(id);

            if (post == null)
            {
                return this.HttpNotFound();
            }

            var model = this.mappingService.Map<PostViewModel>(post);

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var post = this.postsData.Find(model.Id);

                post.Title = model.Title;
                post.Content = this.sanitizer.Sanitize(model.Content);
                post.CategoryId = model.CategoryId;

                this.postsData.Update(post);
                this.postsData.SaveChanges();

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

            var post = this.postsData.Find(id);

            return this.View(post);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (this.ModelState.IsValid)
            {
                this.postsData.Remove(id);
                this.postsData.SaveChanges();
            }

            return this.RedirectToAction("Index");
        }
    }
}