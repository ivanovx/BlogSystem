namespace BlogSystem.Services.Data.Posts
{
    using System.Linq;

    using BlogSystem.Data.Models;
    using BlogSystem.Common;

    public interface IPostsDataService
    {
        IQueryable<Post> GetAllPosts();

        IQueryable<Post> GetAllPostsByCategory(int id);

        IQueryable<Post> GetLatestPosts(int size = GlobalConstants.DefaultPageSize);

        IQueryable<Post> GetPagePosts(int page, int perPage);

        Post GetPost(int id);
    }
}