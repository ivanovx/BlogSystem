namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    
    using BlogSystem.Common;
    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;
    using BlogSystem.Web.Infrastructure.XSS;
    using BlogSystem.Web.Infrastructure.Identity;
    using BlogSystem.Web.Infrastructure.Populators;
    using BlogSystem.Web.Areas.Administration.ViewModels.Posts;

    public class PostsController : AdministrationController
    {
        private readonly IDbRepository<Post> postsData;
        private readonly IDbRepository<Category> categoriesData;
        private readonly ICurrentUser currentUser;
        private readonly IDropDownListPopulator populator;
        private readonly ISanitizer sanitizer;

        public PostsController(IDbRepository<Post> postsData, IDbRepository<Category> categoriesData,
            ICurrentUser currentUser, IDropDownListPopulator populator, ISanitizer sanitizer) 
        {
            this.postsData = postsData;
            this.categoriesData = categoriesData;
            this.currentUser = currentUser;
            this.populator = populator;
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

            var posts = this.mapper.Map<PostViewModel>(postsPage).ToList();

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
            var model = new PostViewModel
            {
                Categories = this.populator.GetCategories()
            };

            return this.View(model);
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
            var post = this.postsData.Find(id);

            if (post == null)
            {
                return this.HttpNotFound();
            }

            var model = this.mapper.Map<PostViewModel>(post);

            model.Categories = this.populator.GetCategories();

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