namespace BlogSystem.Web.ViewModels.Page
{
    using System;
    using AutoMapper;
    using BlogSystem.Web.Infrastructure.Mapping;
    using BlogSystem.Data.Models;

    public class PageViewModel : IMapFrom<Page>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Page, PageViewModel>()
                .ForMember(model => model.Author, config => config.MapFrom(page => page.Author.UserName));
        }
    }
}