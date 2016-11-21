

namespace BlogSystem.Web.Areas.Administration.ViewModels.BlogPost
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using BlogSystem.Data.Contracts;
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

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<BlogPost, BlogPostViewModel>()
                .ForMember(model => model.Author, config => config.MapFrom(post => post.Author.Email));

            configuration.CreateMap<BlogPost, BlogPostViewModel>()
                .ForMember(model => model.IsDeleted, config => config.MapFrom(post => post.IsDeleted));

            configuration.CreateMap<BlogPost, BlogPostViewModel>()
                .ForMember(model => model.DeletedOn, config => config.MapFrom(post => post.DeletedOn));

            configuration.CreateMap<BlogPost, BlogPostViewModel>()
                .ForMember(model => model.CreatedOn, config => config.MapFrom(post => post.CreatedOn));

            configuration.CreateMap<BlogPost, BlogPostViewModel>()
                .ForMember(model => model.ModifiedOn, config => config.MapFrom(post => post.ModifiedOn));
        }
    }
}