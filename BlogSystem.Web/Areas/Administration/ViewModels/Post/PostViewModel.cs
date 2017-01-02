using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BlogSystem.Data.Contracts;

namespace BlogSystem.Web.Areas.Administration.ViewModels.Post
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Administration;

    public class PostViewModel : AdministrationViewModel, IMapFrom<Post>, IMapTo<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [UIHint("tinymce_full")]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        //public string Author { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            /*config.CreateMap<Post, PostViewModel>()
                .ForMember(m => m.Author, c => c.MapFrom(post => post.Author.UserName));*/
        }
    }
}