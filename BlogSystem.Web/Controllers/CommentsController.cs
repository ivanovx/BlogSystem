namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Net;
    using System.Web.Mvc;
    using BlogSystem.Data.Models;
    using BlogSystem.Data.UnitOfWork;
    using BlogSystem.Web.InputModels.Comments;

    [Authorize]
    public class CommentsController : BaseController
    {
        public CommentsController(IBlogSystemData data) 
            : base(data)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, CreateCommentInputModel commentInputModel)
        {
            if (this.ModelState.IsValid)
            {
                var comment = new PostComment
                {
                    BlogPostId = id,
                    Content = commentInputModel.Content,
                    User = this.UserProfile, 
                    UserId = this.UserProfile.Id
                };

                this.Data.PostComments.Add(comment);
                this.Data.SaveChanges();

                return this.RedirectToAction("Post", "Blog", new
                {
                    id = id
                });
            }

            return this.Content("Content is required");
        }

        [HttpGet]
        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comment = this.Data.PostComments.Find(id);

            if (comment == null)
            {
                return this.HttpNotFound();
            }

            if (comment.UserId != this.UserProfile.Id)
            {
                return this.HttpNotFound();
            }

            var model = new EditCommentInputModel
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                BlogPostId = comment.BlogPostId,
                UserId = comment.UserId
            };

            return this.View(model);
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCommentInputModel commentInputModel)
        {
            if (this.ModelState.IsValid)
            {
                var comment = this.Data.PostComments.Find(commentInputModel.Id);

                comment.Id = commentInputModel.Id;
                comment.Content = commentInputModel.Content;
                comment.CreatedOn = commentInputModel.CreatedOn;
                comment.BlogPostId = commentInputModel.BlogPostId;
                comment.UserId = commentInputModel.UserId;

                this.Data.PostComments.Update(comment);
                this.Data.SaveChanges();

                return this.RedirectToAction("Post", "Blog", new
                {
                    id = commentInputModel.BlogPostId
                });
            }

            return this.View(commentInputModel);
        }
    }
}