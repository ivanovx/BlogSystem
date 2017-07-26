namespace BlogSystem.Data
{
    using System;
    using System.Linq;
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using BlogSystem.Data.Contracts;
    using BlogSystem.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(nameOrConnectionString: "DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Post> Posts { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Page> Pages { get; set; }

        public virtual IDbSet<Setting> Settings { get; set; }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletableEntityRules();

            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo) entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (
                var entry in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity) entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }

        public new IDbSet<TEntity> Set<TEntity>() 
            where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasRequired(c => c.Author).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Page>().HasRequired(c => c.Author).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Comment>().HasRequired(c => c.Author).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Comment>().HasRequired(c => c.Post).WithMany().WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}