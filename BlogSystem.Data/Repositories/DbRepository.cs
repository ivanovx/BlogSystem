namespace BlogSystem.Data.Repositories
{
    using System.Linq;
    using System.Data.Entity;

    public class DbRepository<T> : IDbRepository<T> 
        where T : class
    {
        private readonly DbContext dbContext;
        private readonly IDbSet<T> entityDbSet;

        public DbRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entityDbSet = this.dbContext.Set<T>();
        }

        //private IDbSet<T> EntitySet { get; }

        public IQueryable<T> All()
        {
            return this.entityDbSet;
        }

        public T Find(object id)
        {
            return this.entityDbSet.Find(id);
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
                this.entityDbSet.Attach(entity);
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