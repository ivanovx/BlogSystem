using System;
using BlogSystem.Data.Models;
using BlogSystem.Web.Infrastructure.Mapping;
using AutoMapper;

namespace BlogSystem.Web.Areas.Administration.ViewModels.PostComments
{
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

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<PostComment, PostCommentViewModel>()
                .ForMember(model => model.User, config => config.MapFrom(post => post.User.Email));

            configuration.CreateMap<PostComment, PostCommentViewModel>()
                .ForMember(model => model.IsDeleted, config => config.MapFrom(post => post.IsDeleted));

            configuration.CreateMap<PostComment, PostCommentViewModel>()
                .ForMember(model => model.DeletedOn, config => config.MapFrom(post => post.DeletedOn));

            configuration.CreateMap<PostComment, PostCommentViewModel>()
                .ForMember(model => model.CreatedOn, config => config.MapFrom(post => post.CreatedOn));

            configuration.CreateMap<PostComment, PostCommentViewModel>()
                .ForMember(model => model.ModifiedOn, config => config.MapFrom(post => post.ModifiedOn));
        }
    }
}