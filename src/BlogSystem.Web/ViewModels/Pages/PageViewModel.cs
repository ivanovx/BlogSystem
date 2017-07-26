namespace BlogSystem.Web.ViewModels.Pages
{
    using AutoMapper;

    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class PageViewModel : BaseViewModel, IMapFrom<Page>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Slug { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Page, PageViewModel>().ForMember(model => model.Author, config => config.MapFrom(page => page.Author.UserName));
        }
    }
}