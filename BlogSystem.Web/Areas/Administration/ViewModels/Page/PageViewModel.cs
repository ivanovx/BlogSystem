using System.Web.Mvc;
using BlogSystem.Web.Areas.Administration.ViewModels.Administration;

namespace BlogSystem.Web.Areas.Administration.ViewModels.Page
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class PageViewModel : AdministrationViewModel, IMapFrom<Page>, IMapTo<Page>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [UIHint("tinymce_full")]
        public string Content { get; set; }

        public string Permalink { get; set; }

        public string AuthorId { get; set; }

        public bool VisibleInMenu { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            /*config.CreateMap<Page, PageViewModel>()
                .ForMember(m => m.Author, c => c.MapFrom(p => p.Author.UserName));*/
        }
    }
}