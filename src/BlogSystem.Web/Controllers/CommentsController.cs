namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using BlogSystem.Web.ViewModels.Comments;

    using BlogSystem.Web.Infrastructure.XSS;
    using BlogSystem.Web.Infrastructure.Attributes;

    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    [Authorize]
    public class CommentsController : BaseController
    {
        private readonly IDbRepository<Comment> commentsData;
        private readonly ISanitizer sanitizer;

        public CommentsController(IDbRepository<Comment> commentsData, ISanitizer sanitizer)
        {
            this.commentsData = commentsData;
            this.sanitizer = sanitizer;
        }

        [AjaxOnly]
        [AllowAnonymous]
        public PartialViewResult All(int id)
        {
            var comments = this.commentsData.All().Where(c => c.PostId == id);
            var model = this.Mapper.Map<CommentViewModel>(comments).ToList();

            return this.PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var userId = this.CurrentUser.GetUser().Id;
                var content = this.sanitizer.Sanitize(model.Content);

                var comment = new Comment
                {
                    PostId = model.Id,
                    Content = content,
                    AuthorId = userId
                };

                this.commentsData.Add(comment);
                this.commentsData.SaveChanges();

                return this.RedirectToAction("All", new { id = model.Id });
            }

            return this.Json("Content is required");
        }
    }
}