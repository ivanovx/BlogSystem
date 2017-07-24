namespace BlogSystem.Data.Repositories
{
    using System.Linq;
    using System.Data.Entity;

    public class DbRepository<TEntity> : IDbRepository<TEntity> 
        where TEntity : class
    {
        private readonly DbContext dbContext;
        private readonly IDbSet<TEntity> dbSet;

        public DbRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> All()
        {
            return this.dbSet.AsQueryable();
        }

        public TEntity Find(object id)
        {
            return this.dbSet.Find(id);
        }

        public TEntity Add(TEntity entity)
        {
            return this.ChangeState(entity, EntityState.Added);
        }

        public TEntity Update(TEntity entity)
        {
            return this.ChangeState(entity, EntityState.Modified);
        }

        public void Remove(TEntity entity)
        {
            this.ChangeState(entity, EntityState.Deleted);
        }

        public TEntity Remove(object id)
        {
            var entity = this.Find(id);

            this.Remove(entity);

            return entity;
        }

        public int SaveChanges()
        {
           return this.dbContext.SaveChanges();
        }

        private TEntity ChangeState(TEntity entity, EntityState state)
        {
            var entry = this.dbContext.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
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