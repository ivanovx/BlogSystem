namespace BlogSystem.Web.ViewModels.Home
{
    using System;
    using System.Web.Mvc;
    
    using AutoMapper;

    using BlogSystem.Data.Models;

    using BlogSystem.Web.Infrastructure.Mapping;
    using BlogSystem.Web.Infrastructure.Helpers.Url;
    
    public class PostConciseViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        private readonly IUrlGenerator urlGenerator;

        public PostConciseViewModel() : this(new UrlGenerator())
        {

        }

        public PostConciseViewModel(IUrlGenerator urlGenerator)
        {
            this.urlGenerator = urlGenerator;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Author { get; set; }

        public string Url => this.urlGenerator.GenerateUrl(this.Title);

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<Post, PostConciseViewModel>().ForMember(m => m.Author, c => c.MapFrom(post => post.Author.UserName));
        }
    }
}