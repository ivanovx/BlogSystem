namespace BlogSystem.Web.ViewModels.Comment
{
    using System;

    using AutoMapper;

    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class CommentViewModel : IMapFrom<PostComment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int BlogPostId { get; set; }

        public string UserId { get; set; }

        public string User { get; set; }

        public DateTime CommentedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<PostComment, CommentViewModel>()
                .ForMember(model => model.CommentedOn, config => config.MapFrom(post => post.CreatedOn));
            configuration.CreateMap<PostComment, CommentViewModel>()
                .ForMember(model => model.User, config => config.MapFrom(post => post.User.UserName));
        }
    }
}