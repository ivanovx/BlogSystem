namespace BlogSystem.Data.UnitOfWork
{
    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    public interface IBlogSystemData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<BlogPost> Posts { get; }

        IRepository<PostComment> PostComments { get; }

        int SaveChanges();
    }
}