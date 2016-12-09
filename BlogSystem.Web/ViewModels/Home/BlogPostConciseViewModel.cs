namespace BlogSystem.Web.ViewModels.Home
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class BlogPostConciseViewModel : IMapFrom<BlogPost>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMMM dd, yyyy, HH:mm:ss tt}")]
        public DateTime CreatedOn { get; set; }

        public int CommentsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<BlogPost, BlogPostConciseViewModel>()
                .ForMember(model => model.Author, config => config.MapFrom(post => post.Author.UserName));

            configuration.CreateMap<BlogPost, BlogPostConciseViewModel>()
                .ForMember(model => model.CommentsCount, config => config.MapFrom(post => post.Comments.Count));
        }
    }
}