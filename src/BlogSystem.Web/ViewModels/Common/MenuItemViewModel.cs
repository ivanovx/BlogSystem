namespace BlogSystem.Web.ViewModels.Common
{
    using BlogSystem.Data.Models;

    using BlogSystem.Web.Infrastructure.Mapping;

    public class MenuItemViewModel : IMapFrom<Page>
    {
        public string Title { get; set; }

        public string Permalink { get; set; }
    }
}