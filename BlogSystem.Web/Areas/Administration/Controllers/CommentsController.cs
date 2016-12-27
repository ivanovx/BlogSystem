namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Common;
    using Data.UnitOfWork;
    using InputModels.PostComment;
    using Infrastructure.Mapping;
    using ViewModels.Comment;

    public class CommentsController : AdministrationController
    {
        private readonly IBlogSystemData data;

        public CommentsController(IBlogSystemData data)
        {
            this.data = data;
        }

        // GET: Administration/Comments
        public ActionResult Index(int page = 1, int perPage = GlobalConstants.DefaultPageSize)
        {
            int pagesCount = (int) Math.Ceiling(this.data.Comments.All().Count() / (decimal) perPage);

            var comments = this.data.Comments
                .All()
                .Where(c => !c.IsDeleted)
                .OrderByDescending(p => p.CreatedOn)
                .To<PostCommentViewModel>()
                .Skip(perPage*(page - 1))
                .Take(perPage)
                .ToList();

            var model = new IndexCommentsPageViewModel
            {
                Comments = comments,
                CurrentPage = page,
                PagesCount = pagesCount,
            };

            return this.View(model);
        }

        // GET: Administration/Comments/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comment = this.data.Comments.Find(id);

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

        // POST: Administration/Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditPostCommentInputModel commentInputModel)
        {
            if (this.ModelState.IsValid)
            {
                var comment = this.data.Comments.Find(commentInputModel.Id);

                comment.Id = commentInputModel.Id;
                comment.PostId = commentInputModel.BlogPostId;
                comment.Content = commentInputModel.Content;
                comment.CreatedOn = commentInputModel.CreatedOn;
                comment.UserId = commentInputModel.UserId;

                this.data.Comments.Update(comment);
                this.data.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(commentInputModel);
        }

        // GET: Administration/Comments/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comment = this.data.Comments.Find(id);

            if (comment == null)
            {
                return this.HttpNotFound();
            }

            return this.View(comment);
        }

        // POST: Administration/Comments/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var comment = this.data.Comments.Find(id);

            this.data.Comments.Remove(comment);
            this.data.SaveChanges();

            return this.RedirectToAction("Index");
        }
    }
}