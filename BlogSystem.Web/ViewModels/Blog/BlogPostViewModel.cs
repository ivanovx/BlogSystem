using System.ComponentModel.DataAnnotations;

namespace BlogSystem.Web.ViewModels.Blog
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Comment;
    using System.Web.Mvc;

    public class BlogPostViewModel : IMapFrom<Post>, IHaveCustomMappings
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

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Post, BlogPostViewModel>()
                .ForMember(model => model.Author, config => config.MapFrom(post => post.Author.UserName));

            configuration.CreateMap<Post, BlogPostViewModel>()
                .ForMember(model => model.Comments, config => config.MapFrom(post => post.Comments));
        }
    }
}