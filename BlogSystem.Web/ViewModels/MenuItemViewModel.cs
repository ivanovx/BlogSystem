namespace BlogSystem.Web.ViewModels
{
    using BlogSystem.Web.Infrastructure.Mapping;

    public class MenuItemViewModel: IMapFrom<Data.Models.Page>
    {
        public string Title { get; set; }

        public string Permalink { get; set; }
    }
}