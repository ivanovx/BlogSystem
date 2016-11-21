
namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System;
    using BlogSystem.Common;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using BlogSystem.Data.Models;
    using BlogSystem.Data.UnitOfWork;
    using BlogSystem.Web.Areas.Administration.ViewModels.BlogPost;
    using BlogSystem.Web.Infrastructure.Mapping;


    public class BlogPostsController : AdministrationController
    {
        public BlogPostsController(IBlogSystemData data)
            : base(data)
        {
        }

        // GET: Administration/BlogPosts
        public ActionResult Index(int page = 1, int perPage = GlobalConstants.PostsPerPageDefaultValue)
        {
            int pagesCount = (int) Math.Ceiling(this.Data.Posts.All().Count() / (decimal)perPage);

            var posts = this.Data.Posts
                .All()
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.CreatedOn)
                .To<BlogPostViewModel>()
                .Skip(perPage * (page - 1))
                .Take(perPage);

            var model = new IndexPageViewModel
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
        public ActionResult Create(BlogPost blogPost)
        {
            if (blogPost != null)
            {
                if (this.ModelState.IsValid)
                {
                    BlogPost post = new BlogPost
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

            return this.View(post);
        }

        // POST: Administration/BlogPosts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogPost blogPost)
        {
            if (this.ModelState.IsValid)
            {
                this.Data.Posts.Update(blogPost);
                this.Data.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(blogPost);
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