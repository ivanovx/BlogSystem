namespace BlogSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using BlogSystem.Data.Models;

    public interface IApplicationDbContext : IDisposable
    {
        IDbSet<BlogPost> Posts { get; set; }

        IDbSet<PostComment> PostComments { get; set; }

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
