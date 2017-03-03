namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Common;
    using ViewModels.Home;
    using BlogSystem.Services.Web.Mapping;
    using BlogSystem.Services.Data.Contracts;

    public class HomeController : BaseController
    {
        private readonly IPostsDataService postsData;
        private readonly IMappingService mappingService;

        public HomeController(IPostsDataService postsData, IMappingService mappingService)
        {
            this.postsData = postsData;
            this.mappingService = mappingService;
        }

        public ActionResult Index(int page = 1, int perPage = GlobalConstants.DefaultPageSize)
        {
            var pagesCount = (int) Math.Ceiling(this.postsData.GetAll().Count() / (decimal) perPage);
            var posts = this.postsData.GetPagePosts(page, perPage);
            var postsPage = this.mappingService.Map<PostConciseViewModel>(posts).ToList();

            var model = new IndexPageViewModel
            {
                Posts = postsPage,
                CurrentPage = page,
                PagesCount = pagesCount
            };

            return this.View(model);
        }
    }
}