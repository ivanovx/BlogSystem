namespace BlogSystem.Web.ViewModels
{
    using Infrastructure.Mapping;

    public class MenuItemViewModel: IMapFrom<Data.Models.Page>
    {
        public string Title { get; set; }

        public string Permalink { get; set; }
    }
}