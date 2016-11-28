using System.Linq;
using System.Web.Mvc;
using BlogSystem.Data.UnitOfWork;
using BlogSystem.Web.Infrastructure.Mapping;
using BlogSystem.Web.ViewModels;
using BlogSystem.Web.ViewModels.Blog;

namespace BlogSystem.Web.Controllers
{
    public class SidebarController : BaseController
    {
        public SidebarController(IBlogSystemData data) 
            : base(data)
        {
        }

        public PartialViewResult Sidebar()
        {
            var posts = this.Data.Posts
                .All()
                .To<BlogPostViewModel>()
                .Take(5)
                .ToList();

            return this.PartialView("_Sidebar", new SidebarViewModel
            {
                RecentPosts = posts
            });
        }
    }
}