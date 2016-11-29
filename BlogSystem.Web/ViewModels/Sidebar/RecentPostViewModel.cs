namespace BlogSystem.Web.ViewModels.Sidebar
{
    using System;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class RecentPostViewModel : IMapFrom<BlogPost>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}