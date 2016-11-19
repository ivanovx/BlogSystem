using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using BlogSystem.Data.Models;

namespace BlogSystem.Data
{
    using System;

    public interface IApplicationDbContext : IDisposable
    {
        IDbSet<BlogPost> Posts { get; set; }

        IDbSet<PostComment> PostComments { get; set; }

        //int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
