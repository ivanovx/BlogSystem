namespace BlogSystem.Web.Areas.Administration.ViewModels.Page
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class PageViewModel : IMapFrom<Data.Models.Page>, IHaveCustomMappings
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

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Data.Models.Page, PageViewModel>()
                .ForMember(model => model.Author, config => config.MapFrom(post => post.Author.UserName));
        }
    }
}