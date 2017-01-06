using BlogSystem.Web.ViewModels.Blog;
using BlogSystem.Web.ViewModels.Page;

namespace BlogSystem.Web.ViewModels.Sidebar
{
    using System.Collections.Generic;

    public class SidebarViewModel
    {
        public IEnumerable<PostViewModel> RecentPosts { get; set; }

        public IEnumerable<PageViewModel> Pages { get; set; }
    }
}