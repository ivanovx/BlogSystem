using BlogSystem.Services.Data.Pages;
using BlogSystem.Services.Data.Posts;

namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels.Sidebar;
    using ViewModels.Posts;
    using ViewModels.Pages;
    using BlogSystem.Services.Web.Caching;
    using BlogSystem.Services.Web.Mapping;
    using BlogSystem.Common;


    public class SidebarController : BaseController
    {
        private readonly IPostsDataService postsData;
        private readonly IPagesDataService pagesData;
        private readonly ICacheService cacheService;
        private readonly IMappingService mappingService;

        public SidebarController(IPostsDataService postsData, IPagesDataService pagesData, 
            ICacheService cacheService, IMappingService mappingService)
        {
            this.postsData = postsData;
            this.pagesData = pagesData;
            this.cacheService = cacheService;
            this.mappingService = mappingService;
        }

        [ChildActionOnly]
        public PartialViewResult Index()
        {
            var posts = this.postsData.GetAll().Take(GlobalConstants.DefaultPageSize);
            var pages = this.pagesData.GetAll();

            var model = new SidebarViewModel
            {
                RecentPosts = this.cacheService.Get("RecentPosts", () => this.mappingService.Map<PostViewModel>(posts).ToList(), 600),
                AllPages = this.cacheService.Get("AllPages", () => this.mappingService.Map<PageViewModel>(pages).ToList(), 600)
            };

            return this.PartialView(model);
        }
    }
}