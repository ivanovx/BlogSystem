namespace BlogSystem.Web.Areas.Administration.ViewModels.Comment
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class PostCommentViewModel: IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int PostId { get; set; }
        
        public string Post { get; set; }

        public string User { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<Comment, PostCommentViewModel>()
                .ForMember(m => m.User, c => c.MapFrom(comment => comment.User.UserName))
                .ForMember(m => m.Post, c => c.MapFrom(comment => comment.Post.Title));
        }
    }
}