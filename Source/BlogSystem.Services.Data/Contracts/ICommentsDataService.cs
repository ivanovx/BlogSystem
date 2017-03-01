namespace BlogSystem.Services.Data.Contracts
{
    using System.Linq;
    using BlogSystem.Data.Models;

    public interface ICommentsDataService
    {
        IQueryable<Comment> GetAllByPost(int id);

        Comment AddCommentToPost(int postId, string content, string userId);
    }
}