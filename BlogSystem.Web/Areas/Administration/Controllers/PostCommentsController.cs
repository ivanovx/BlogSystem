namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using BlogSystem.Data.Models;
    using BlogSystem.Data.UnitOfWork;
    using BlogSystem.Web.Areas.Administration.InputModels.PostComment;
    using BlogSystem.Common;
    using BlogSystem.Web.Areas.Administration.ViewModels.PostComments;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class PostCommentsController : AdministrationController
    {
        public PostCommentsController(IBlogSystemData data) 
            : base(data)
        {
        }

        // GET: Administration/PostComments
        public ActionResult Index(int page = 1, int perPage = GlobalConstants.CommentsPerPageDefaultValue)
        {
            int pagesCount = (int) Math.Ceiling(this.Data.PostComments.All().Count() / (decimal) perPage);

            var comments = this.Data.PostComments
                .All()
                .Where(c => !c.IsDeleted)
                .OrderByDescending(p => p.CreatedOn)
                .Include(c => c.BlogPost) // Todo
                .Include(c => c.User)
                .To<PostCommentViewModel>()
                .Skip(perPage*(page - 1))
                .Take(perPage);

            var model = new IndexPageViewModel
            {
                Comments = comments.ToList(),
                CurrentPage = page,
                PagesCount = pagesCount,
            };

            return this.View(model);
        }

        // GET: Administration/PostComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PostComment postComment = this.Data.PostComments.Find(id);

            if (postComment == null)
            {
                return this.HttpNotFound();
            }

            return this.View(postComment);
        }

        // GET: Administration/PostComments/Edit/5
        [HttpGet]
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

            var model = new EditPostCommentInputModel
            {
                Id = comment.Id,
                BlogPostId = comment.BlogPostId,
                UserId = comment.UserId,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn
            };

            return this.View(model);
        }

        // POST: Administration/PostComments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditPostCommentInputModel commentInputModel)
        {
            if (this.ModelState.IsValid)
            {
                var comment = this.Data.PostComments.Find(commentInputModel.Id);

                comment.Id = commentInputModel.Id;
                comment.BlogPostId = commentInputModel.BlogPostId;
                comment.Content = commentInputModel.Content;
                comment.CreatedOn = commentInputModel.CreatedOn;
                comment.UserId = commentInputModel.UserId;
                comment.ModifiedOn = DateTime.Now;

                /*this.Data.PostComments.Update(postComment);
                this.Data.SaveChanges();*/

                this.Data.PostComments.Update(comment);
                this.Data.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(commentInputModel);
        }

        // GET: Administration/PostComments/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PostComment postComment = this.Data.PostComments.Find(id);

            if (postComment == null)
            {
                return this.HttpNotFound();
            }

            return this.View(postComment);
        }

        // POST: Administration/PostComments/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostComment postComment = this.Data.PostComments.Find(id);

            this.Data.PostComments.Remove(postComment);
            this.Data.SaveChanges();

            return this.RedirectToAction("Index");
        }
    }
}