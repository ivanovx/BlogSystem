namespace BlogSystem.Web.ViewModels.Sidebar
{
    using System.Collections.Generic;

    public class SidebarViewModel
    {
        public IEnumerable<RecentPostViewModel> RecentPosts { get; set; }
    }
}