namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Services.Data.Comments;
    using Services.Web.Mapping;
    using Infrastructure.Identity;
    using Infrastructure.XSS;
    using ViewModels.Comments;

    [Authorize]
    public class CommentsController : BaseController
    {
        private readonly ICommentsDataService commentsData;
        private readonly ICurrentUser currentUser;
        private readonly IMappingService mappingService;
        private readonly ISanitizer sanitizer;

        public CommentsController(ICommentsDataService commentsData, ICurrentUser currentUser, 
            IMappingService mappingService, ISanitizer sanitizer)
        {
            this.commentsData = commentsData;
            this.currentUser = currentUser;
            this.mappingService = mappingService;
            this.sanitizer = sanitizer;
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult All(int id)
        {
            var comments = this.commentsData.GetAllCommentsByPost(id);
            var model = this.mappingService.Map<CommentViewModel>(comments).ToList();

            return this.PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, CommentViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var userId = this.currentUser.GetUser().Id;
                var content = this.sanitizer.Sanitize(model.Content);

                this.commentsData.AddCommentToPost(id, content, userId);

                return this.RedirectToAction("All", new { id });
            }

            return this.Json("Content is required");
        }
    }
}