namespace BlogSystem.Web.Areas.Administration.ViewModels.Page
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class PageViewModel : IMapFrom<Page>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [DataType(DataType.Html)]
        public string Content { get; set; }

        public string Permalink { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Page, PageViewModel>()
                .ForMember(model => model.Author, config => config.MapFrom(post => post.Author.UserName));
        }
    }
}