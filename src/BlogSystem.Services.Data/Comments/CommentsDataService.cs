namespace BlogSystem.Services.Data.Comments
{
    using System.Linq;
    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    public class CommentsDataService : ICommentsDataService
    {
        private readonly IDbRepository<Comment> comments;

        public CommentsDataService(IDbRepository<Comment> comments)
        {
            this.comments = comments;
        }

        public IQueryable<Comment> GetAllByPost(int id)
        {
            return this.comments.All().Where(c => !c.IsDeleted && c.PostId == id).OrderByDescending(c => c.CreatedOn);
        }

        public Comment AddCommentToPost(int postId, string content, string userId)
        {
            var comment = new Comment
            {
                PostId = postId,
                Content = content,
                UserId = userId
            };

            this.comments.Add(comment);
            this.comments.SaveChanges();

            return comment;
        }
    }
}