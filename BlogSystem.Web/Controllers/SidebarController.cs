namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using Infrastructure.Mapping;
    using ViewModels.Sidebar;

    public class SidebarController : BaseController
    {
        public SidebarController(IBlogSystemData data) 
            : base(data)
        {
        }

        [ChildActionOnly]
        public PartialViewResult Index()
        {
            var recentPosts = this.Data.Posts.All().OrderByDescending(p => p.CreatedOn).To<RecentPostViewModel>().Take(5);

            var model = new SidebarViewModel
            {
                RecentPosts = recentPosts.ToList()
            };

            return this.PartialView(model);
        }
    }
}