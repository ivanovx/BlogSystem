namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Common;
    using ViewModels.Home;
    using Services.Data.Posts;
    using Services.Web.Mapping;

    public class HomeController : BaseController
    {
        private readonly IPostsDataService postsData;

        public HomeController(IPostsDataService postsData)
        {
            this.postsData = postsData;
        }

        public ActionResult Index(int page = 1, int perPage = GlobalConstants.DefaultPageSize)
        {
            var pagesCount = (int) Math.Ceiling(this.postsData.GetAllPosts().Count() / (decimal) perPage);
            var postsPage = this.postsData.GetPagePosts(page, perPage);
            var posts = this.mapper.Map<PostConciseViewModel>(postsPage).ToList();

            var model = new IndexPageViewModel
            {
                Posts = posts,
                CurrentPage = page,
                PagesCount = pagesCount
            };

            return this.View(model);
        }
    }
}