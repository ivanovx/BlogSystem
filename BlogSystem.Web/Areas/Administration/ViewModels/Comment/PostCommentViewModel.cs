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
        
        public virtual Post Post { get; set; }

        public string User { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, PostCommentViewModel>()
                .ForMember(model => model.User, config => config.MapFrom(comment => comment.User.Email));
        }
    }
}