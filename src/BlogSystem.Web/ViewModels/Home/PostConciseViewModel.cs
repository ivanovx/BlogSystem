namespace BlogSystem.Web.ViewModels.Home
{
    using System;
    using System.Web.Mvc;
    
    using AutoMapper;

    using BlogSystem.Data.Models;

    using BlogSystem.Web.Infrastructure.Mapping;
    
    public class PostConciseViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Slug { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<Post, PostConciseViewModel>().ForMember(m => m.Author, c => c.MapFrom(post => post.Author.UserName));
        }
    }
}