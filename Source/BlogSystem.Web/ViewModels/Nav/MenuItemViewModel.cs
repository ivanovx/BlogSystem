namespace BlogSystem.Web.ViewModels.Nav
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class MenuItemViewModel: IMapFrom<Page>
    {
        public string Title { get; set; }

        public string Permalink { get; set; }
    }
}