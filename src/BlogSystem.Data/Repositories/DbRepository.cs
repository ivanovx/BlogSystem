namespace BlogSystem.Data.Repositories
{
    using System.Linq;
    using System.Data.Entity;

    public class DbRepository<T> : IDbRepository<T> 
        where T : class
    {
        private readonly DbContext dbContext;
        private readonly IDbSet<T> entitySet;

        public DbRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entitySet = this.dbContext.Set<T>();
        }

        public IQueryable<T> All()
        {
            return this.entitySet.AsQueryable();
        }

        public T Find(object id)
        {
            return this.entitySet.Find(id);
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
                this.entitySet.Attach(entity);
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