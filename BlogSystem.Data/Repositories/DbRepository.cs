namespace BlogSystem.Data.Repositories
{
    using System.Data.Entity;
    using System.Linq;

    public class DbRepository<T> : IDbRepository<T> 
        where T : class
    {
        private readonly DbContext dbContext;

        public DbRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.EntitySet = dbContext.Set<T>();
        }

        private IDbSet<T> EntitySet { get; }

        public IQueryable<T> All()
        {
            return this.EntitySet;
        }

        public T Find(object id)
        {
            return this.EntitySet.Find(id);
        }

        public T Add(T entity)
        {
            return this.ChangeState(entity, EntityState.Added);
        }

        public T Update(T entity)
        {
            return this.ChangeState(entity, EntityState.Modified);
        }

        public void Remove(T entity)
        {
            this.ChangeState(entity, EntityState.Deleted);
        }

        public T Remove(object id)
        {
            var entity = this.Find(id);

            this.Remove(entity);

            return entity;
        }

        public int SaveChanges()
        {
           return this.dbContext.SaveChanges();
        }

        private T ChangeState(T entity, EntityState state)
        {
            var entry = this.dbContext.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.EntitySet.Attach(entity);
            }

            entry.State = state;

            return entity;
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
        }
    }
}