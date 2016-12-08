namespace BlogSystem.Web.Areas.Administration.ViewModels.PostComments
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class PostCommentViewModel: IMapFrom<PostComment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int BlogPostId { get; set; }
    
        public virtual Data.Models.BlogPost BlogPost { get; set; }

        public string User { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<PostComment, PostCommentViewModel>()
                .ForMember(model => model.User, config => config.MapFrom(comment => comment.User.Email));
        }
    }
}