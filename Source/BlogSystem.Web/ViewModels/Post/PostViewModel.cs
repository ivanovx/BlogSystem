﻿namespace BlogSystem.Web.ViewModels.Post
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Comment;

    public class PostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [AllowHtml]
        [DataType(DataType.Html)]
        public string Content { get; set; }

        public string Author { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime CreatedOn { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<Post, PostViewModel>()
                .ForMember(m => m.Author, c => c.MapFrom(post => post.Author.UserName))
                .ForMember(m => m.Comments, c => c.MapFrom(post => post.Comments));
        }
    }
}