namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using BlogSystem.Services.Data.Pages;
    using BlogSystem.Services.Data.Posts;

    using BlogSystem.Web.ViewModels.Common;
    using BlogSystem.Web.ViewModels.Posts;
    using BlogSystem.Web.ViewModels.Pages;

    public class SidebarController : BaseController
    {
        private readonly IPostsDataService postsData;
        private readonly IPagesDataService pagesData;

        public SidebarController(IPostsDataService postsData, IPagesDataService pagesData)
        {
            this.postsData = postsData;
            this.pagesData = pagesData;
        }

        [ChildActionOnly]
        [OutputCache(Duration = 10 * 60)]
        public PartialViewResult Index()
        {
            var latestPosts = this.postsData.GetLatestPosts();
            var allPages = this.pagesData.GetAllPages();

            var model = new SidebarViewModel
            {
                RecentPosts = this.Cache.Get("LatestPosts", () => {
                    return this.Mapper.Map<PostViewModel>(latestPosts).ToList();
                }, 600),
                AllPages = this.Cache.Get("AllPages", () => {
                    return this.Mapper.Map<PageViewModel>(allPages).ToList();
                }, 600)
            };

            return this.PartialView(model);
        }
    }
}