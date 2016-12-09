namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Common;
    using Data.Models;
    using Data.UnitOfWork;
    using ViewModels.BlogPost;
    using InputModels.BlogPost;
    using Infrastructure.Mapping;

    public class BlogPostsController : AdministrationController
    {
        public BlogPostsController(IBlogSystemData data)
            : base(data)
        {
        }

        // GET: Administration/BlogPosts
        public ActionResult Index(int page = 1, int perPage = GlobalConstants.PostsPerPageDefaultValue)
        {
            int pagesCount = (int) Math.Ceiling(this.Data.Posts.All().Count() / (decimal) perPage);

            var posts = this.Data.Posts
                .All()
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.CreatedOn)
                .To<BlogPostViewModel>()
                .Skip(perPage * (page - 1))
                .Take(perPage);

            var model = new IndexPostsPageViewModel
            {
                Posts = posts.ToList(),
                CurrentPage = page,
                PagesCount = pagesCount,
            };

            return this.View(model);
        }

        // GET: Administration/BlogPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var post = this.Data.Posts.Find(id);

            if (post == null)
            {
                return this.HttpNotFound();
            }

            return this.View(post);
        }

        // GET: Administration/BlogPosts/Create
        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/BlogPosts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogPostCreateInputModel blogPost)
        {
            if (blogPost != null)
            {
                if (this.ModelState.IsValid)
                {
                    var post = new Post
                    {
                        Title = blogPost.Title,
                        Content = blogPost.Content,
                        Author = this.UserProfile,
                        AuthorId = this.UserProfile.Id,
                        CreatedOn = DateTime.Now
                    };

                    this.Data.Posts.Add(post);
                    this.Data.SaveChanges();

                    return this.RedirectToAction("Index");
                }
            }

            return this.View(blogPost);
        }

        // GET: Administration/BlogPosts/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var post = this.Data.Posts.Find(id);

            if (post == null)
            {
                return this.HttpNotFound();
            }

            var model = new BlogPostEditInputModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedOn = post.CreatedOn,
                AuthorId = post.AuthorId
            };


            return this.View(model);
        }

        // POST: Administration/BlogPosts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogPostEditInputModel postInputModel)
        {
            if (this.ModelState.IsValid)
            {
                var post = this.Data.Posts.Find(postInputModel.Id);

                post.Id = postInputModel.Id;
                post.Title = postInputModel.Title;
                post.Content = postInputModel.Content;
                post.AuthorId = postInputModel.AuthorId;
                post.CreatedOn = postInputModel.CreatedOn;
                post.ModifiedOn = DateTime.Now;

                this.Data.Posts.Update(post);
                this.Data.SaveChanges();

                /*this.Data.Posts.Update(blogPost);
                this.Data.SaveChanges();*/

                return this.RedirectToAction("Index");
            }

            return this.View(postInputModel);
        }

        // GET: Administration/BlogPosts/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var blogPost = this.Data.Posts.Find(id);

            if (blogPost == null)
            {
                return this.HttpNotFound();
            }

            return this.View(blogPost);
        }

        // POST: Administration/BlogPosts/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var blogPost = this.Data.Posts.Find(id);

            this.Data.Posts.Remove(blogPost);
            this.Data.SaveChanges();

            return this.RedirectToAction("Index");
        }
    }
}