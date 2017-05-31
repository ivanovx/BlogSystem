namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Services.Data.Pages;
    using Services.Data.Posts;
    using Services.Web.Caching;
    using Services.Web.Mapping;
    using ViewModels.Common;
    using ViewModels.Posts;
    using ViewModels.Pages;
    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    public class SidebarController : BaseController
    {
        private readonly IPostsDataService postsData;
        private readonly IPagesDataService pagesData;
        private readonly ICacheService cacheService;
        private readonly IMappingService mappingService;

        private readonly IDbRepository<Category> categoriesData;

        public SidebarController(IPostsDataService postsData, IPagesDataService pagesData,
           IDbRepository<Category> categoriesData, ICacheService cacheService, 
           IMappingService mappingService)
        {
            this.postsData = postsData;
            this.pagesData = pagesData;
            this.categoriesData = categoriesData;
            this.cacheService = cacheService;
            this.mappingService = mappingService;
        }

        [ChildActionOnly]
        public PartialViewResult Index()
        {
            var posts = this.postsData.GetLatestPosts();
            var pages = this.pagesData.GetAllPages();
            var categories = this.categoriesData.All();

            var model = new SidebarViewModel
            {
                RecentPosts = this.cacheService.Get("RecentPosts",
                    () => this.mappingService.Map<PostViewModel>(posts).ToList(), 600),
                AllPages = this.cacheService.Get("AllPages",
                    () => this.mappingService.Map<PageViewModel>(pages).ToList(), 600),
                Categories = this.mappingService.Map<CategoryViewModel>(categories).ToList()
            };

            return this.PartialView(model);
        }
    }
}