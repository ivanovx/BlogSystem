namespace BlogSystem.Web.ViewModels.Posts
{
    using AutoMapper;

    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.Mapping;
    
    public class PostViewModel : BaseViewModel, IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Slug { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<Post, PostViewModel>().ForMember(m => m.Author, c => c.MapFrom(post => post.Author.UserName));
        }
    }
}