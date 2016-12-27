using BlogSystem.Web.ViewModels.Blog;

namespace BlogSystem.Web.ViewModels.Sidebar
{
    using System.Collections.Generic;

    public class SidebarViewModel
    {
        public IEnumerable<PostViewModel> RecentPosts { get; set; }
    }
}