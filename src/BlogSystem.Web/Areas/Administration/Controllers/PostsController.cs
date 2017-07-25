namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Web.Mvc;
    
    using BlogSystem.Common;
    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    using BlogSystem.Web.Areas.Administration.ViewModels.Posts;

    using BlogSystem.Web.Infrastructure.Helpers.Url;

    public class PostsController : AdministrationController
    {
        private readonly IDbRepository<Post> postsData;
        private readonly IUrlGenerator urlGenerator;

        public PostsController(IDbRepository<Post> postsData, IUrlGenerator urlGenerator)
        { 
            this.postsData = postsData;
            this.urlGenerator = urlGenerator;
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

            var posts = this.Mapper.Map<PostViewModel>(postsPage).ToList();

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
                    Content = model.Content,
                    Slug = this.urlGenerator.GenerateUrl(model.Title),
                    AuthorId = this.CurrentUser.GetUser().Id
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var post = this.postsData.Find(id);

            if (post == null)
            {
                return this.HttpNotFound();
            }

            var model = this.Mapper.Map<PostViewModel>(post);

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
                post.Content = model.Content;
                post.Slug = this.urlGenerator.GenerateUrl(model.Title);
                post.AuthorId = this.CurrentUser.GetUser().Id;

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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var post = this.postsData.Find(id);

            if (post == null)
            {
                return this.HttpNotFound();
            }

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