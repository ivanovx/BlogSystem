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
    using ViewModel = ViewModels.Comment.CommentViewModel;

    public class CommentsController : GenericAdministrationController<EntityModel, ViewModel>
    { 
        public CommentsController(IDbRepository<EntityModel> dataRepository) 
            : base(dataRepository)
        {
        }

        // GET: Administration/Comments
        public ActionResult Index(int page = 1, int perPage = GlobalConstants.DefaultPageSize)
        {
            int pagesCount = (int) Math.Ceiling(this.dataRepository.All().Count() / (decimal) perPage);

            var comments = this.GetAll().Skip(perPage * (page - 1)).Take(perPage).ToList();

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
    }
}