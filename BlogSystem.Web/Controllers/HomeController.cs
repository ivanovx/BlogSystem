namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using BlogSystem.Data.UnitOfWork;
    using BlogSystem.Web.ViewModels.Home;

    using BlogSystem.Common;

    using PagedList;

    public class HomeController : BaseController
    {
        public HomeController(IBlogSystemData data) : base(data)
        { 
        }

        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;

            var posts = this.Data.Posts.All().OrderByDescending(p => p.CreatedOn).ProjectTo<BlogPostConciseViewModel>();
            var viewModel = new IndexPageViewModel
            {
                Posts = posts
            };
            var model = viewModel.Posts.ToPagedList(pageNumber, GlobalConstants.PostsPerPageDefaultValue);

            return this.View(model);
        }
    }
}