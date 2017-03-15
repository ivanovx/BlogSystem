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

        public IQueryable<Post> GetAll()
        {
            var posts = this.posts
                .All()
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.CreatedOn);

            return posts;
        }

        public IQueryable<Post> GetLatest()
        {
            var posts = this.GetAll().Take(GlobalConstants.DefaultPageSize);

            return posts;
        }

        public IQueryable<Post> GetPagePosts(int page, int perPage)
        {
            return this.GetAll().Skip(perPage * (page - 1)).Take(perPage);
        }

        public Post GetPost(int id)
        {
            return this.posts.All().FirstOrDefault(p => !p.IsDeleted && p.Id == id);
        }
    }
}