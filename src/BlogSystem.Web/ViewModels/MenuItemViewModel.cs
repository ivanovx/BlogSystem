namespace BlogSystem.Web.ViewModels
{
    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class MenuItemViewModel : IMapFrom<Page>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }
    }
}