using System.Collections.Generic;

namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Configuration;
    using BlogSystem.Data.UnitOfWork;
    using BlogSystem.Web.Infrastructure.Mapping;
    using BlogSystem.Web.ViewModels.Sidebar;
    using BlogSystem.Web.ViewModels;
    using BlogSystem.Web.ViewModels.Blog;


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
        
        // todo
        [NonAction]
        private IDictionary<string, string> SocialMedia()
        {
            string facebook = ConfigurationManager.AppSettings["facebook"];

            return new Dictionary<string, string>()
            {
                { "Facebook", facebook },
            };
        }
    }
}