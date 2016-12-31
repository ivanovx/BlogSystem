namespace BlogSystem.Web.ViewModels.Sidebar
{
    using System.Collections.Generic;
    using Data.Models;
    using Blog;

    public class SidebarViewModel
    {
        public IEnumerable<PostViewModel> RecentPosts { get; set; }

        public IEnumerable<Page> Pages { get; set; }
    }
}