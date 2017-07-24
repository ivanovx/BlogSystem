namespace BlogSystem.Data.Repositories
{
    using System;
    using System.Linq;

    public interface IDbRepository<TEntity> : IDisposable 
        where TEntity : class
    {
        IQueryable<TEntity> All();

        TEntity Find(object id);

        TEntity Add(TEntity entity);

        TEntity Update(TEntity entity);

        TEntity Remove(object id);

        void Remove(TEntity entity);

        int SaveChanges();
    }
}