namespace BlogSystem.Web.ViewModels.Common
{
    using System.Collections.Generic;
    using Posts;
    using Pages;
    using BlogSystem.Data.Models;

    public class SidebarViewModel
    {
        public IEnumerable<PostViewModel> RecentPosts { get; set; }

        public IEnumerable<PageViewModel> AllPages { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}