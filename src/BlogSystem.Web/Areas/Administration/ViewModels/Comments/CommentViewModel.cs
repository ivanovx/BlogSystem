namespace BlogSystem.Web.Areas.Administration.ViewModels.Comments
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Administration;

    public class CommentViewModel: AdministrationViewModel, IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [UIHint("tinymce_full")]
        public string Content { get; set; }

        public string Author { get; set; }
        
        [Required]
        public string AuthorId { get; set; }

        public string Post { get; set; }

        [Required]
        public int PostId { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<Comment, CommentViewModel>()
                .ForMember(m => m.Author, c => c.MapFrom(comment => comment.Author.UserName))
                .ForMember(m => m.Post, c => c.MapFrom(comment => comment.Post.Title));
        }
    }
}