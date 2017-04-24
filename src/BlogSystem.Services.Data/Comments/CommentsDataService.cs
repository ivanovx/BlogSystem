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

        public IQueryable<Comment> GetAllCommentsByPost(int id)
        {
            var comments = this.comments
                .All()
                .Where(c => !c.IsDeleted && c.PostId == id)
                .OrderByDescending(c => c.CreatedOn);

            return comments;
        }

        public Comment AddCommentToPost(int postId, string content, string userId)
        {
            var comment = new Comment
            {
                PostId = postId,
                Content = content,
                AuthorId = userId
            };

            this.comments.Add(comment);
            this.comments.SaveChanges();

            return comment;
        }
    }
}