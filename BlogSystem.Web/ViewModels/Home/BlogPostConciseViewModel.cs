using System.Web.Mvc;

namespace BlogSystem.Web.ViewModels.Home
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class BlogPostConciseViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [AllowHtml]
        [DataType(DataType.Html)]
        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CommentsCount { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Post, BlogPostConciseViewModel>()
                .ForMember(model => model.Author, config => config.MapFrom(post => post.Author.UserName));

            configuration.CreateMap<Post, BlogPostConciseViewModel>()
                .ForMember(model => model.CommentsCount, config => config.MapFrom(post => post.Comments.Count));
        }
    }
}