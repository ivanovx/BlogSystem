namespace BlogSystem.Data.Repositories
{
    using System;
    using System.Linq;

    public interface IDbRepository<T> : IDisposable 
        where T : class
    {
        IQueryable<T> All();

        T Find(object id);

        T Add(T entity);

        T Update(T entity);

        T Remove(object id);

        void Remove(T entity);

        int SaveChanges();
    }
}