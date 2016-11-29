namespace BlogSystem.Web.Areas.Administration.ViewModels.BlogPost
{
    using System;
    using AutoMapper;
    using BlogSystem.Web.Infrastructure.Mapping;
    using BlogSystem.Data.Models;

    public class BlogPostViewModel : IMapFrom<BlogPost>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<BlogPost, BlogPostViewModel>()
                .ForMember(model => model.Author, config => config.MapFrom(post => post.Author.Email));
        }
    }
}