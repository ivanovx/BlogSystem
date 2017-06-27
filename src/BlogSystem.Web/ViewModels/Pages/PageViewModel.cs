namespace BlogSystem.Web.ViewModels.Pages
{
    using System;
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using BlogSystem.Data.Models;

    using BlogSystem.Web.Infrastructure.Mapping;

    public class PageViewModel : BaseViewModel, IMapFrom<Page>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public string Permalink { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Page, PageViewModel>()
                .ForMember(model => model.Author, config => config.MapFrom(page => page.Author.UserName));
        }
    }
}