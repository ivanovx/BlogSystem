namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using ViewModels.Comments;
    using Data.Repositories;
    using Infrastructure;
    using Infrastructure.Identity;
    using Infrastructure.Extensions;

    [Authorize]
    public class CommentsController : BaseController
    {
        private readonly IDbRepository<Comment> commentsRepository;
        private readonly ICurrentUser currentUser;

        public CommentsController(IDbRepository<Comment> commentsRepository, ICurrentUser currentUser)
        {
            this.commentsRepository = commentsRepository;
            this.currentUser = currentUser;
        }

        [AllowAnonymous]
        public PartialViewResult All(int id)
        {
            var comments = this.commentsRepository
                .All()
                .Where(c => c.PostId == id && !c.IsDeleted)
                .OrderByDescending(c => c.CreatedOn)
                .To<CommentViewModel>();

            return this.PartialView(comments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, CommentViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var comment = new Comment
                {
                    Content = model.Content,
                    PostId = id,
                    User = this.currentUser.Get(),
                    UserId = this.currentUser.Get().Id
                };

                this.commentsRepository.Add(comment);
                this.commentsRepository.SaveChanges();

                return this.RedirectToAction("All", new { id });
            }

            return this.Json("Content is required");
        }
    }
}