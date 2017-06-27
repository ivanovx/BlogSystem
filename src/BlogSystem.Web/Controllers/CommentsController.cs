namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using BlogSystem.Services.Data.Comments;

    using BlogSystem.Web.ViewModels.Comments;
    using BlogSystem.Web.Infrastructure.XSS;
    using BlogSystem.Web.Infrastructure.Attributes;

    [Authorize]
    public class CommentsController : BaseController
    {
        private readonly ICommentsDataService commentsData;
        private readonly ISanitizer sanitizer;

        public CommentsController(ICommentsDataService commentsData, ISanitizer sanitizer)
        {
            this.commentsData = commentsData;
            this.sanitizer = sanitizer;
        }

        [HttpGet]
        [AjaxOnly]
        [AllowAnonymous]
        public PartialViewResult All(int id)
        {
            var comments = this.commentsData.GetAllCommentsByPost(id);
            var model = this.Mapper.Map<CommentViewModel>(comments).ToList();

            return this.PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, CommentViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var userId = this.CurrentUser.GetUser().Id;
                var content = this.sanitizer.Sanitize(model.Content);

                this.commentsData.AddCommentToPost(id, content, userId);

                return this.RedirectToAction("All", new { id });
            }

            return this.Json("Content is required");
        }
    }
}