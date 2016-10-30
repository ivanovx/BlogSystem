namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using BlogSystem.Data.Models;
    using BlogSystem.Data.UnitOfWork;

    using PagedList;

    public class PostCommentsController : AdministrationController
    {
        private const int CommentsPerPageDefaultValue = 7;

        public PostCommentsController(IBlogSystemData data)
            : base(data)
        {
        }

        // GET: Administration/PostComments
        public ActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;

            var postComments =
                this.Data.PostComments.All()
                    .OrderByDescending(p => p.CreatedOn)
                    .Include(p => p.BlogPost)
                    .Include(p => p.User)
                    .ToList();
            var model = new PagedList<PostComment>(postComments, pageNumber, CommentsPerPageDefaultValue);

            return this.View(model);
        }

        // GET: Administration/PostComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var postComment = this.Data.PostComments.Find(id);
            if (postComment == null)
            {
                return this.HttpNotFound();
            }

            return this.View(postComment);
        }

        // GET: Administration/PostComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var postComment = this.Data.PostComments.Find(id);
            if (postComment == null)
            {
                return this.HttpNotFound();
            }

            return this.View(postComment);
        }

        // POST: Administration/PostComments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,Content,BlogPostId,UserId,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] PostComment
                postComment)
        {
            if (this.ModelState.IsValid)
            {
                this.Data.PostComments.Update(postComment);
                this.Data.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(postComment);
        }

        // GET: Administration/PostComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var postComment = this.Data.PostComments.Find(id);
            if (postComment == null)
            {
                return this.HttpNotFound();
            }

            return this.View(postComment);
        }

        // POST: Administration/PostComments/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostComment postComment = this.Data.PostComments.Find(id);
            this.Data.PostComments.Remove(postComment);
            this.Data.SaveChanges();

            return this.RedirectToAction("Index");
        }
    }
}