namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels.Comments;
    using Infrastructure.Identity;
    using BlogSystem.Services.Data.Contracts;
    using BlogSystem.Services.Web.Mapping;

    [Authorize]
    public class CommentsController : BaseController
    {
        private readonly ICommentsDataService commentsData;
        private readonly ICurrentUser currentUser;
        private readonly IMappingService mappingService;

        public CommentsController(ICommentsDataService commentsData, ICurrentUser currentUser, IMappingService mappingService)
        {
            this.commentsData = commentsData;
            this.currentUser = currentUser;
            this.mappingService = mappingService;
        }

        [AllowAnonymous]
        public PartialViewResult All(int id)
        {
            var comments = this.commentsData.GetAllByPost(id);
            var model = this.mappingService.Map<CommentViewModel>(comments).ToList();

            return this.PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, CommentViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var userId = this.currentUser.Get().Id;

                this.commentsData.AddCommentToPost(id, model.Content, userId);

                return this.RedirectToAction("All", new { id });
            }

            return this.Json("Content is required");
        }
    }
}