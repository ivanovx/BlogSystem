namespace BlogSystem.Data.UnitOfWork
{
    using Models;
    using Repositories;

    public interface IBlogSystemData
    {
        IDbRepository<ApplicationUser> Users { get; }

        IDbRepository<Post> Posts { get; }

        IDbRepository<Comment> Comments { get; }

        IDbRepository<Page> Pages { get; }

        int SaveChanges();
    }
}