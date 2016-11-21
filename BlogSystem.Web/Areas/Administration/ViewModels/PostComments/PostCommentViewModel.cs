using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogSystem.Data.Models;
using BlogSystem.Web.Infrastructure.Mapping;
using AutoMapper;

namespace BlogSystem.Web.Areas.Administration.ViewModels.PostComments
{
    public class PostCommentViewModel: IMapFrom<PostComment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int BlogPostId { get; set; }

        public virtual Data.Models.BlogPost BlogPost { get; set; }

        public string User { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<PostComment, PostCommentViewModel>()
                .ForMember(model => model.User, config => config.MapFrom(post => post.User.Email));
        }
    }
}