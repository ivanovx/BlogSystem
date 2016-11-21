namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Net;
    using System.Web.Mvc;
    using BlogSystem.Data.Models;
    using BlogSystem.Data.UnitOfWork;
    using BlogSystem.Web.InputModels.Comment;

    [Authorize]
    public class CommentsController : BaseController
    {
        public CommentsController(IBlogSystemData data) 
            : base(data)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, CommentInputModel comment)
        {
            if (this.ModelState.IsValid)
            {
                var newComment = new PostComment
                {
                    Content = comment.Content, 
                    BlogPostId = id, 
                    User = this.UserProfile, 
                    UserId = this.UserProfile.Id,
                    CreatedOn = DateTime.Now
                };

                this.Data.PostComments.Add(newComment);
                this.Data.SaveChanges();

                return this.RedirectToAction("Post", "Blog", new { id });
            }

            return this.Content("Content is required");
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var postComment = this.Data.PostComments.Find(id);

            if (postComment == null)
            {
                return this.HttpNotFound();
            }

            if (postComment.UserId != this.UserProfile.Id)
            {
                return this.HttpNotFound();
            }

            return this.View(postComment);
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostComment postComment)
        {
            if (this.ModelState.IsValid)
            {
                this.Data.PostComments.Update(postComment);
                this.Data.SaveChanges();

                return this.RedirectToAction("Post", "Blog", new
                {
                    id = postComment.BlogPostId
                });
            }

            return this.View(postComment);
        }
    }
}