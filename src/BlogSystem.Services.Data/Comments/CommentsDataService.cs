namespace BlogSystem.Services.Data.Comments
{
    using System.Linq;
    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    public class CommentsDataService : ICommentsDataService
    {
        private readonly IDbRepository<Comment> commentsData;

        public CommentsDataService(IDbRepository<Comment> commentsData)
        {
            this.commentsData = commentsData;
        }

        public IQueryable<Comment> GetAllCommentsByPost(int id)
        {
            var comments = this.commentsData
                .All()
                .Where(c => !c.IsDeleted && c.PostId == id)
                .OrderByDescending(c => c.CreatedOn)
                .AsQueryable();

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

            this.commentsData.Add(comment);
            this.commentsData.SaveChanges();

            return comment;
        }
    }
}