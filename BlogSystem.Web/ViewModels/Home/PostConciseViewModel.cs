namespace BlogSystem.Web.ViewModels.Home
{
    using System;
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    
    public class PostConciseViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        public string Content { get; set; }

        public string Author { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime CreatedOn { get; set; }

        public int CommentsCount { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Post, PostConciseViewModel>()
                .ForMember(model => model.Author, config => config.MapFrom(post => post.Author.UserName))
                .ForMember(model => model.CommentsCount, config => config.MapFrom(post => post.Comments.Count));
        }
    }
}