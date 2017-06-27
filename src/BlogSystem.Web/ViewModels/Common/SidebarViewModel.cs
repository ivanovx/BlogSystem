namespace BlogSystem.Web.ViewModels.Common
{
    using System.Collections.Generic;

    using BlogSystem.Web.ViewModels.Posts;
    using BlogSystem.Web.ViewModels.Pages;

    public class SidebarViewModel
    {
        public IEnumerable<PostViewModel> RecentPosts { get; set; }

        public IEnumerable<PageViewModel> AllPages { get; set; }
    }
}