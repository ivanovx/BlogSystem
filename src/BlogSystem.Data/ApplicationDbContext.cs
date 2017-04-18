namespace BlogSystem.Data
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Contracts;
    using Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(nameOrConnectionString: "DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Post> Posts { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Page> Pages { get; set; }

        public IDbSet<Setting> Settings { get; set; }

        //public static ApplicationDbContext Create() => new ApplicationDbContext();

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();

            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            var entitySet = this.ChangeTracker
                .Entries()
                .Where(e => e.Entity is IAuditInfo && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entitySet)
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

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}