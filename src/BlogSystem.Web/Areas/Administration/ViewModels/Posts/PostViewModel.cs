namespace BlogSystem.Web.Areas.Administration.ViewModels.Posts
{
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;
   
    using AutoMapper;

    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.Mapping;
    using BlogSystem.Web.Areas.Administration.ViewModels.Administration;

    public class PostViewModel : AdministrationViewModel, IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [UIHint("TinyMCE")]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<Post, PostViewModel>().ForMember(m => m.Author, c => c.MapFrom(p => p.Author.UserName));
        }
    }
}