namespace BlogSystem.Services.Data.Contracts
{
    using System.Linq;
    using BlogSystem.Data.Models;

    public interface IPostsDataService
    {
        IQueryable<Post> GetAll();

        IQueryable<Post> GetPagePosts(int page, int perPage);

        Post GetPost(int id);
    }
}