using BlogSystem.Web.ViewModels.Blog;

namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using Infrastructure.Mapping;
    using ViewModels.Sidebar;

    public class SidebarController : BaseController
    {
        private readonly IBlogSystemData data;

        public SidebarController(IBlogSystemData data)
        {
            this.data = data;
        }

        [ChildActionOnly]
        [OutputCache(Duration = 10 * 60)]
        public PartialViewResult Index()
        {
            /*var recentPosts = this.data
                .Posts
                .All()
                .OrderByDescending(p => p.CreatedOn)
                .To<PostViewModel>()
                .Take(5)
                .ToList();

            var model = new SidebarViewModel
            {
                RecentPosts = recentPosts
            };*/

            //return this.PartialView(model);

            var model = new SidebarViewModel
            {
                RecentPosts = this.Cache.Get(
                    "RecentBlogPosts",
                    () =>
                        this.data.Posts
                            .All()
                            .OrderByDescending(p => p.CreatedOn)
                            .To<PostViewModel>()
                            .Take(5)
                            .ToList(),
                    600)
            };

            return this.PartialView(model);
        }
    }
}