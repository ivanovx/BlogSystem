namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Common;
    using Data.Repositories;
    using ViewModels.Comments;
    using Services.Web.Mapping;
    using Data.Models;
    using Infrastructure.XSS;

    public class CommentsController : AdministrationController
    {
        private readonly IDbRepository<Comment> commentsData;
        private readonly IMappingService mappingService;
        private readonly ISanitizer sanitizer;

        public CommentsController(IDbRepository<Comment> commentsData, IMappingService mappingService, ISanitizer sanitizer) 
        {
            this.commentsData = commentsData;
            this.mappingService = mappingService;
            this.sanitizer = sanitizer;
        }

        [HttpGet]
        public ActionResult Index(int page = 1, int perPage = GlobalConstants.DefaultPageSize)
        {
            int pagesCount = (int) Math.Ceiling(this.commentsData.All().Count() / (decimal) perPage);

            var commentsPage = this.commentsData
                .All()
                .OrderByDescending(p => p.CreatedOn)
                .Skip(perPage * (page - 1))
                .Take(perPage);

            var comments = this.mappingService.Map<CommentViewModel>(commentsPage).ToList();

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
                return this.HttpNotFound();
            }

            var comment = this.commentsData.Find(id);
            var model = this.mappingService.Map<CommentViewModel>(comment);

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
                return this.HttpNotFound();
            }

            var comment = this.commentsData.Find(id);

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