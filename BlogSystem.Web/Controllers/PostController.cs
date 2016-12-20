namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using ViewModels.Blog;
    using Infrastructure.Mapping;

    public class PostController : BaseController
    {
        public PostController(IBlogSystemData data) 
            : base(data)
        {
        }

        public  ActionResult Index(int? id)
        {
            var post = this.Data.Posts.Find(id);

            if (post == null)
            {
                return this.HttpNotFound("Blog post not foud!");
            }

            var model = this.Data.Posts.All().Where(p => p.Id == id).To<BlogPostViewModel>().FirstOrDefault();

            return this.View(model);
        }
    }
}