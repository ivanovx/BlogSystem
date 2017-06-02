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
        public PartialViewResult Index()
        {
            var posts = this.postsData.GetLatestPosts();
            var pages = this.pagesData.GetAllPages();
            var categories = this.categoriesData.GetAllCategories();

            var model = new SidebarViewModel
            {
                RecentPosts = this.cache.Get("RecentPosts", () => {
                    return this.mapping.Map<PostViewModel>(posts).ToList();
                }, 600),
                AllPages = this.cache.Get("AllPages", () => {
                    return this.mapping.Map<PageViewModel>(pages).ToList();
                }, 600),
                Categories = this.mapping.Map<CategoryViewModel>(categories).ToList()
            };

            return this.PartialView(model);
        }
    }
}