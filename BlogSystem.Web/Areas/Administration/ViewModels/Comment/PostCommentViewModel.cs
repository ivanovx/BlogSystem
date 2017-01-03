using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BlogSystem.Web.Areas.Administration.ViewModels.Administration;

namespace BlogSystem.Web.Areas.Administration.ViewModels.Comment
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class PostCommentViewModel: AdministrationViewModel, IMapFrom<Comment>, IMapTo<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }
   
        public string PostTitle { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [UIHint("tinymce_full")]
        public string Content { get; set; }

        public string UserName { get; set; }

        [Required]
        public string UserId { get; set; }

       /* public DateTime? ModifiedOn { get; set; }

        public DateTime CreatedOn { get; set; }*/

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<Comment, PostCommentViewModel>()
                .ForMember(m => m.UserName, c => c.MapFrom(comment => comment.User.UserName))
                .ForMember(m => m.PostTitle, c => c.MapFrom(comment => comment.Post.Title));
        }
    }
}