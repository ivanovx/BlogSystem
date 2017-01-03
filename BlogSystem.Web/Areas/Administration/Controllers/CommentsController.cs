namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Common;
    using Data.Repositories;
    using ViewModels.Comment;
    using Base;

    using EntityModel = Data.Models.Comment;
    using ViewModel = ViewModels.Comment.PostCommentViewModel;

    public class CommentsController : GenericAdministrationController<EntityModel, ViewModel>
    {
        private readonly IDbRepository<EntityModel> dataRepository;

        public CommentsController(IDbRepository<EntityModel> dataRepository) 
            : base(dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        // GET: Administration/Comments
        public ActionResult Index(int page = 1, int perPage = GlobalConstants.DefaultPageSize)
        {
            int pagesCount = (int) Math.Ceiling(this.dataRepository.All().Count() / (decimal) perPage);

            var comments = this.GetAll()
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

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var entity = this.dataRepository.Find(id);

            if (entity != null)
            {
                var model = this.Mapper.Map<ViewModel>(entity);

                return this.View(model);
            }

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ViewModel model)
        {
            var entity = this.FindAndUpdateEntity(model.Id, model);

            if (entity != null)
            {
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var model = this.dataRepository.Find(id);

            return this.View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.DestroyEntity(id);

            return this.RedirectToAction("Index");
        }

        /*// GET: Administration/Comments/Edit/5
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
        }*/
    }
}