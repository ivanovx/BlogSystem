namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using BlogSystem.Data.UnitOfWork;
    using BlogSystem.Web.Infrastructure.Mapping;
    using BlogSystem.Web.ViewModels.Sidebar;

    public class SidebarController : BaseController
    {
        public SidebarController(IBlogSystemData data) 
            : base(data)
        {
        }

        [ChildActionOnly]
        public PartialViewResult Sidebar()
        {
            var recentPosts = this.Data.Posts
                .All()
                .OrderByDescending(p => p.CreatedOn)
                .To<RecentPostViewModel>()
                .Take(5);

            var model = new SidebarViewModel
            {
                RecentPosts = recentPosts.ToList()
            };

            return this.PartialView("_Sidebar", model);
        }
    }
}