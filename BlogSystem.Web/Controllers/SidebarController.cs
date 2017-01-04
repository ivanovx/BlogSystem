namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using ViewModels.Sidebar;
    using ViewModels.Blog;
    using Infrastructure.Extensions;

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
            var model = new SidebarViewModel
            {
                RecentPosts = this.Cache.Get("RecentBlogPosts",
                    () =>
                        this.data.Posts
                            .All()
                            .Where(p => !p.IsDeleted)
                            .OrderByDescending(p => p.CreatedOn)
                            .To<PostViewModel>()
                            .Take(5)
                            .ToList(),
                    600),
                Pages = this.data.Pages.All().ToList()
            };

            return this.PartialView(model);
        }
    }
}