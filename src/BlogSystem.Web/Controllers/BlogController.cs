namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    using BlogSystem.Web.ViewModels;
    using BlogSystem.Web.ViewModels.Pages;
    using BlogSystem.Web.ViewModels.Posts;

    using BlogSystem.Web.Infrastructure.Attributes;
    using BlogSystem.Common;

    public class BlogController : BaseController
    {
        private readonly IDbRepository<Post> postsData;
        private readonly IDbRepository<Page> pagesData;

        public BlogController(IDbRepository<Post> postsData, IDbRepository<Page> pagesData)
        {
            this.postsData = postsData;
            this.pagesData = pagesData;
        }

        [PassRouteValuesToViewData]
        public ActionResult Post(string slug, int id)
        {
            var post = this.postsData.All().Where(p => !p.IsDeleted && p.Slug == slug && p.Id == id).FirstOrDefault();

            if (post == null)
            {
                return this.HttpNotFound();
            }

            var model = this.Mapper.Map<PostViewModel>(post);

            return this.View(model);
        }

        public ActionResult Page(string slug, int id)
        {
            var page = this.pagesData.All().Where(p => !p.IsDeleted && p.Slug == slug && p.Id == id).FirstOrDefault();

            if (page == null)
            {
                return this.HttpNotFound();
            }

            var model = this.Mapper.Map<PageViewModel>(page);

            return this.View(model);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 6 * 10 * 60)]
        public PartialViewResult Menu()
        {
            var pages = this.pagesData.All().Where(p => !p.IsDeleted && p.ShowInMenu);
            var model = this.Cache.Get("Menu", () => this.Mapper.Map<MenuItemViewModel>(pages).ToList(), 600);

            return this.PartialView("_Menu", model);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 6 * 10 * 60)]
        public PartialViewResult Sidebar()
        {
            var allPages = this.pagesData.All().Where(p => !p.IsDeleted);
            var recentPosts = this.postsData.All().Where(p => !p.IsDeleted).OrderByDescending(p => p.CreatedOn).Take(GlobalConstants.DefaultPageSize);

            var model = new SidebarViewModel
            {
                RecentPosts = this.Cache.Get("LatestPosts", () => this.Mapper.Map<PostViewModel>(recentPosts).ToList(), 600),
                AllPages = this.Cache.Get("AllPages", () => this.Mapper.Map<PageViewModel>(allPages).ToList(), 600)
            };

            return this.PartialView("_Sidebar", model);
        }
    }
}