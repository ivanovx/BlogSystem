namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Data.Models;
    using Data.UnitOfWork;
    using InputModels.Comments;
    using Infrastructure.Identity;
    using Infrastructure.Mapping;
    using ViewModels.Comment;

    [Authorize]
    public class CommentsController : BaseController
    {
        private readonly IBlogSystemData data;
        private readonly ICurrentUser currentUser;

        public CommentsController(IBlogSystemData data, ICurrentUser currentUser)
        {
            this.data = data;
            this.currentUser = currentUser;
        }

        [ChildActionOnly]
        public PartialViewResult All(int id)
        {
            var comments = this.data.Comments
                .All()
                .Where(c => c.PostId == id && !c.IsDeleted)
                .OrderByDescending(c => c.CreatedOn)
                .To<CommentViewModel>();

            return this.PartialView(comments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, CommentViewModel commentInputModel)
        {
            if (this.ModelState.IsValid)
            {
                var comment = new Comment
                {
                    Content = commentInputModel.Content,
                    PostId = id,
                    User = this.currentUser.Get(),
                    UserId = this.currentUser.Get().Id
                };

                this.data.Comments.Add(comment);
                this.data.SaveChanges();

                return this.RedirectToAction("Post", "Posts", new
                {
                    id = id
                });
            }

            return this.Content("Content is required");
        }
    }
}