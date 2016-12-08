namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using ViewModels.Blog;
    using Infrastructure.Mapping;

    public class BlogController : BaseController
    {
        public BlogController(IBlogSystemData data) 
            : base(data)
        {
        }

        // GET: Blog/Post/5
        public  ActionResult Post(int? id)
        {
            var post = this.Data.Posts.Find(id);

            if (post == null)
            {
                return this.HttpNotFound("Blog post not foud!");
            }

            var viewModel = this.Data.Posts
                .All()
                .Where(p => p.Id == id)
                .To<BlogPostViewModel>()
                .FirstOrDefault();

            return this.View(viewModel);
        }
    }
}