namespace BlogSystem.Services.Data.Posts
{
    using System.Linq;
    using BlogSystem.Data.Models;

    public interface IPostsDataService
    {
        IQueryable<Post> GetAll();

        IQueryable<Post> GetLatest();

        IQueryable<Post> GetPagePosts(int page, int perPage);

        Post GetPost(int id);
    }
}