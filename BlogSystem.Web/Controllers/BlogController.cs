namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using BlogSystem.Data.UnitOfWork;
    using BlogSystem.Web.ViewModels.Blog;

    public class BlogController : BaseController
    {
        public BlogController(IBlogSystemData data) : base(data)
        {
        }

        // GET: Blog/Post/5
        public ActionResult Post(int? id)
        {
            var post = this.Data.Posts.Find(id);

            if (post == null)
            {
                return this.HttpNotFound("Blog post not foud!");
            }

            var viewModel = this.Data.Posts
                                .All()
                                .Where(p => p.Id == id)
                                .ProjectTo<BlogPostViewModel>()
                                .FirstOrDefault();

            return this.View(viewModel);
        }
    }
}