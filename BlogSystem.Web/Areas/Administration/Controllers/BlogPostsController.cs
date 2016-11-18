using System;

namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using BlogSystem.Data.Models;
    using BlogSystem.Data.UnitOfWork;
    using BlogSystem.Web.Infrastructure;

    using PagedList;

    public class BlogPostsController : AdministrationController
    {
        private const int PostsPerPageDefaultValue = 5;
        private readonly ISanitizer sanitizer;

        public BlogPostsController(IBlogSystemData data, ISanitizer sanitizer) : base(data)
        {
            this.sanitizer = sanitizer;
        }

        // GET: Administration/BlogPosts
        public ActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;

            var blogPosts = this.Data.Posts.All().OrderByDescending(p => p.CreatedOn).Include(b => b.Author).ToList();

            var model = new PagedList<BlogPost>(blogPosts, pageNumber, PostsPerPageDefaultValue);

            return this.View(model);
        }

        // GET: Administration/BlogPosts/Details/5
        public ActionResult Details(int? id)
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

        // GET: Administration/BlogPosts/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/BlogPosts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
       /* public ActionResult Create(
            [Bind(Include = "Id,Title,Content,ShortContent,AuthorId,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] BlogPost blogPost)*/
            public ActionResult Create(BlogPost blogPost)
        {
            if (blogPost != null)
            {
                /* 
                 blogPost.Author = this.UserProfile;
                 blogPost.AuthorId = this.UserProfile.Id;
 
                 if (this.ModelState.IsValid)
                 {
                     blogPost.ShortContent = this.sanitizer.Sanitize(blogPost.ShortContent);
                     blogPost.Content = this.sanitizer.Sanitize(blogPost.Content);
 
                     this.Data.Posts.Add(blogPost);
                     this.Data.SaveChanges();
 
                     return this.RedirectToAction("Index");
                 }*/


                if (this.ModelState.IsValid)
                {
                    var post = new BlogPost
                    {
                        Title = blogPost.Title,
                        Content = this.sanitizer.Sanitize(blogPost.Content),
                        //ShortContent = this.sanitizer.Sanitize(blogPost.Content.ToUpper()),
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
        public ActionResult Edit(int? id)
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

        // POST: Administration/BlogPosts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*public ActionResult Edit(
            [Bind(Include = "Id,Title,Content,AuthorId,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] BlogPost
                blogPost)*/
        public ActionResult Edit(BlogPost blogPost)
        {
            if (this.ModelState.IsValid)
            {
                //this.Data.Posts.Update(blogPost);
                this.Data.Posts.Update(blogPost);
                this.Data.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(blogPost);
        }

        // GET: Administration/BlogPosts/Delete/5
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