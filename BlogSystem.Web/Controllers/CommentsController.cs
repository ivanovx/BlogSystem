namespace BlogSystem.Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using Data.Models;
    using Data.UnitOfWork;
    using InputModels.Comments;

    [Authorize]
    public class CommentsController : BaseController
    {
        public CommentsController(IBlogSystemData data) 
            : base(data)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, CreateCommentInputModel commentInputModel)
        {
            if (this.ModelState.IsValid)
            {
                var comment = new Comment
                {
                    PostId = id,
                    Content = commentInputModel.Content,
                    User = this.UserProfile, 
                    UserId = this.UserProfile.Id
                };

                this.Data.Comments.Add(comment);
                this.Data.SaveChanges();

                return this.RedirectToAction("Post", "Blog", new
                {
                    id = id
                });
            }

            return this.Content("Content is required");
        }

        [HttpGet]
        // GET: Comments/Edit/5
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

            if (comment.UserId != this.UserProfile.Id)
            {
                return this.HttpNotFound();
            }

            var model = new EditCommentInputModel
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                BlogPostId = comment.PostId,
                UserId = comment.UserId
            };

            return this.View(model);
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCommentInputModel commentInputModel)
        {
            if (this.ModelState.IsValid)
            {
                var comment = this.Data.Comments.Find(commentInputModel.Id);

                comment.Id = commentInputModel.Id;
                comment.Content = commentInputModel.Content;
                comment.CreatedOn = commentInputModel.CreatedOn;
                comment.PostId = commentInputModel.BlogPostId;
                comment.UserId = commentInputModel.UserId;

                this.Data.Comments.Update(comment);
                this.Data.SaveChanges();

                return this.RedirectToAction("Post", "Blog", new
                {
                    id = commentInputModel.BlogPostId
                });
            }

            return this.View(commentInputModel);
        }
    }
}