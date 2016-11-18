namespace BlogSystem.Data.UnitOfWork
{
    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    public interface IBlogSystemData
    {
        IDbRepository<ApplicationUser> Users { get; }
        IDbRepository<BlogPost> Posts { get; }
        IDbRepository<PostComment> PostComments { get; }

        int SaveChanges();
    }
}