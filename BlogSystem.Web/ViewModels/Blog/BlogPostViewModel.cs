namespace BlogSystem.Web.ViewModels.Blog
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.Mapping;
    using BlogSystem.Web.ViewModels.Comment;

    public class BlogPostViewModel : IMapFrom<BlogPost>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<BlogPost, BlogPostViewModel>()
                .ForMember(model => model.Author, config => config.MapFrom(post => post.Author.UserName));
            configuration.CreateMap<BlogPost, BlogPostViewModel>()
                .ForMember(model => model.Comments, config => config.MapFrom(post => post.Comments));
        }
    }
}