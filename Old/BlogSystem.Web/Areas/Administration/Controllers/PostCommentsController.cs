namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Common;
    using Data.Models;
    using Data.UnitOfWork;
    using InputModels.PostComment;
    using ViewModels.PostComments;
    using Infrastructure.Mapping;

    public class PostCommentsController : AdministrationController
    {
        public PostCommentsController(IBlogSystemData data) 
            : base(data)
        {
        }

        // GET: Administration/PostComments
        public ActionResult Index(int page = 1, int perPage = GlobalConstants.DefaultPageSize)
        {
            int pagesCount = (int) Math.Ceiling(this.Data.Comments.All().Count() / (decimal) perPage);

            var comments = this.Data.Comments
                .All()
                .Where(c => !c.IsDeleted)
                .OrderByDescending(p => p.CreatedOn)
                .Include(c => c.Post) // Todo
                .Include(c => c.User)
                .To<PostCommentViewModel>()
                .Skip(perPage*(page - 1))
                .Take(perPage);

            var model = new IndexCommentsPageViewModel
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

            var postComment = this.Data.Comments.Find(id);

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

            var comment = this.Data.Comments.Find(id);

            if (comment == null)
            {
                return this.HttpNotFound();
            }

            var model = new EditPostCommentInputModel
            {
                Id = comment.Id,
                BlogPostId = comment.PostId,
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
                var comment = this.Data.Comments.Find(commentInputModel.Id);

                comment.Id = commentInputModel.Id;
                comment.PostId = commentInputModel.BlogPostId;
                comment.Content = commentInputModel.Content;
                comment.CreatedOn = commentInputModel.CreatedOn;
                comment.UserId = commentInputModel.UserId;
                comment.ModifiedOn = DateTime.Now;

                /*this.Data.PostComments.Update(postComment);
                this.Data.SaveChanges();*/

                this.Data.Comments.Update(comment);
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

            Comment postComment = this.Data.Comments.Find(id);

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
            Comment postComment = this.Data.Comments.Find(id);

            this.Data.Comments.Remove(postComment);
            this.Data.SaveChanges();

            return this.RedirectToAction("Index");
        }
    }
}