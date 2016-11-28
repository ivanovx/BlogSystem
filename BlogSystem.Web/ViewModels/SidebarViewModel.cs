using System.Collections.Generic;
using BlogSystem.Web.ViewModels.Blog;

namespace BlogSystem.Web.ViewModels
{
    public class SidebarViewModel
    {
        public IEnumerable<BlogPostViewModel> RecentPosts { get; set; }
    }
}