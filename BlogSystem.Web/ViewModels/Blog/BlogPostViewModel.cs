namespace BlogSystem.Web.ViewModels.Blog
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Comment;

    public class BlogPostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMMM dd, yyyy, HH:mm:ss tt}")]
        public DateTime CreatedOn { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, BlogPostViewModel>()
                .ForMember(model => model.Author, config => config.MapFrom(post => post.Author.UserName));

            configuration.CreateMap<Post, BlogPostViewModel>()
                .ForMember(model => model.Comments, config => config.MapFrom(post => post.Comments));
        }
    }
}