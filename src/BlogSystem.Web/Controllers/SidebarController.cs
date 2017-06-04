namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using BlogSystem.Services.Data.Pages;
    using BlogSystem.Services.Data.Posts;
    using BlogSystem.Services.Data.Categories;

    using BlogSystem.Web.ViewModels.Common;
    using BlogSystem.Web.ViewModels.Posts;
    using BlogSystem.Web.ViewModels.Pages;

    public class SidebarController : BaseController
    {
        private readonly IPostsDataService postsData;
        private readonly IPagesDataService pagesData;
        private readonly ICategoriesDataService categoriesData;

        public SidebarController(IPostsDataService postsData, IPagesDataService pagesData,
           ICategoriesDataService categoriesData)
        {
            this.postsData = postsData;
            this.pagesData = pagesData;
            this.categoriesData = categoriesData;
        }

        [ChildActionOnly]
        [OutputCache(Duration = 10 * 60)]
        public PartialViewResult Index()
        {
            var latestPosts = this.postsData.GetLatestPosts();
            var allPages = this.pagesData.GetAllPages();
            var allCategories = this.categoriesData.GetAllCategories();

            var model = new SidebarViewModel
            {
                RecentPosts = this.cache.Get("LatestPosts", () => {
                    return this.mapper.Map<PostViewModel>(latestPosts).ToList();
                }, 600),
                AllPages = this.cache.Get("AllPages", () => {
                    return this.mapper.Map<PageViewModel>(allPages).ToList();
                }, 600),
                Categories = this.mapper.Map<CategoryViewModel>(allCategories).ToList()
            };

            return this.PartialView(model);
        }
    }
}