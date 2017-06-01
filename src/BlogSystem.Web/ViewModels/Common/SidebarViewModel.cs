namespace BlogSystem.Web.ViewModels.Common
{
    using System.Collections.Generic;
    using Posts;
    using Pages;

    public class SidebarViewModel
    {
        public IEnumerable<PostViewModel> RecentPosts { get; set; }

        public IEnumerable<PageViewModel> AllPages { get; set; }

        //public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}