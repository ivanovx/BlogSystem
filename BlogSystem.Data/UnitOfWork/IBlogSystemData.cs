namespace BlogSystem.Data.UnitOfWork
{
    using Models;
    using Repositories;

    public interface IBlogSystemData
    {
        IDbRepository<Post> Posts { get; }

        IDbRepository<Comment> Comments { get; }

        IDbRepository<Page> Pages { get; }

        IDbRepository<ApplicationUser> Users { get; }

        IDbRepository<Setting> Settings { get; }

        int SaveChanges();
    }
}