namespace BlogSystem.Services.Data
{
    using System.Linq;
    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;
    using BlogSystem.Services.Data.Contracts;

    public class PostsDataService : IPostsDataService
    {
        private readonly IDbRepository<Post> posts;

        public PostsDataService(IDbRepository<Post> posts)
        {
            this.posts = posts;
        }

        public IQueryable<Post> GetAll()
        {
            return this.posts.All().Where(p => !p.IsDeleted).OrderByDescending(p => p.CreatedOn);
        }

        public IQueryable<Post> GetPagePosts(int page, int perPage)
        {
            return this.GetAll().Skip(perPage * (page - 1)).Take(perPage);
        }

        public Post GetPost(int id)
        {
            return this.GetAll().FirstOrDefault(p => p.Id == id);
        }
    }
}