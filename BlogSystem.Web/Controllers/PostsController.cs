namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using ViewModels.Blog;
    using Infrastructure.Mapping;

    public class PostsController : BaseController
    {
        private readonly IBlogSystemData data;

        public PostsController(IBlogSystemData data)
        {
            this.data = data;
        }

        public  ActionResult Details(int? id)
        {
            var post = this.data.Posts.Find(id);

            if (post == null)
            {
                return this.HttpNotFound("Blog post not foud!");
            }

            var model = this.data.Posts
                .All()
                .Where(p => p.Id == id)
                .To<PostViewModel>()
                .FirstOrDefault();

            return this.View(model);
        }
    }
}