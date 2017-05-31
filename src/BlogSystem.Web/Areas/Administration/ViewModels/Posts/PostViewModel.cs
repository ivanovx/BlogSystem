﻿namespace BlogSystem.Web.Areas.Administration.ViewModels.Posts
{
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Administration;
    using System.Collections.Generic;

    public class PostViewModel : AdministrationViewModel, IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [UIHint("tinymce_full")]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        [Display(Name = "Category")]
        [UIHint("DropDownList")]
        public int? CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<Post, PostViewModel>()
                .ForMember(m => m.Author, c => c.MapFrom(p => p.Author.UserName));
        }
    }
}