namespace BlogSystem.Web.ViewModels.Comments
{
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;
   
    using AutoMapper;

    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class CommentViewModel : BaseViewModel, IMapFrom<Comment>, IHaveCustomMappings
    { 
        public int Id { get; set; }

        [AllowHtml]
        [UIHint("TinyMCE")]
        public string Content { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<Comment, CommentViewModel>().ForMember(m => m.Author, c => c.MapFrom(comment => comment.Author.UserName));
        }
    }
}