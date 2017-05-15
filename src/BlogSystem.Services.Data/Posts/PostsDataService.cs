namespace BlogSystem.Services.Data.Posts
{
    using System.Linq;
    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;
    using Common;

    public class PostsDataService : IPostsDataService
    {
        private readonly IDbRepository<Post> posts;

        public PostsDataService(IDbRepository<Post> posts)
        {
            this.posts = posts;
        }

        public IQueryable<Post> GetAllPosts()
        {
            var posts = this.posts
                .All()
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.CreatedOn)
                .AsQueryable();

            return posts;
        }

        public IQueryable<Post> GetLatestPosts(int size = GlobalConstants.DefaultPageSize)
        {
            var posts = this.GetAllPosts()
                .Take(size)
                .AsQueryable();

            return posts;
        }

        public IQueryable<Post> GetPagePosts(int page, int perPage)
        {
            var posts = this.GetAllPosts()
                .Skip(perPage * (page - 1))
                .Take(perPage)
                .AsQueryable();

            return posts;
        }

        public Post GetPost(int id)
        {
            var post = this.GetAllPosts().FirstOrDefault(p => p.Id == id);

            return post;
        }
    }
}