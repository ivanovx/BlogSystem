namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Web.Mvc;

    using Common;
    using Data.Repositories;
    using ViewModels.Comments;
    using Data.Models;
    using Infrastructure.XSS;

    public class CommentsController : AdministrationController
    {
        private readonly IDbRepository<Comment> commentsData;
        private readonly ISanitizer sanitizer;

        public CommentsController(IDbRepository<Comment> commentsData, ISanitizer sanitizer) 
        {
            this.commentsData = commentsData;
            this.sanitizer = sanitizer;
        }

        public ActionResult Index(int page = 1, int perPage = GlobalConstants.DefaultPageSize)
        {
            int pagesCount = (int) Math.Ceiling(this.commentsData.All().Count() / (decimal) perPage);

            var allComments = this.commentsData
                .All()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(p => p.CreatedOn)
                .Skip(perPage * (page - 1))
                .Take(perPage);

            var comments = this.Mapper.Map<CommentViewModel>(allComments).ToList();

            var model = new IndexCommentsPageViewModel
            {
                Comments = comments,
                CurrentPage = page,
                PagesCount = pagesCount,
            };

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
           if (id == null)
           {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
           }

            var comment = this.commentsData.Find(id);

            if (comment == null)
            {
                return this.HttpNotFound();
            }

            var model = this.Mapper.Map<CommentViewModel>(comment);

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CommentViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var comment = this.commentsData.Find(model.Id);

                comment.Content = this.sanitizer.Sanitize(model.Content);

                this.commentsData.Update(comment);
                this.commentsData.SaveChanges();

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

            var comment = this.commentsData.Find(id);

            if (comment == null)
            {
                return this.HttpNotFound();
            }

            return this.View(comment);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (this.ModelState.IsValid)
            {
                this.commentsData.Remove(id);
                this.commentsData.SaveChanges();
            }

            return this.RedirectToAction("Index");
        }
    }
}