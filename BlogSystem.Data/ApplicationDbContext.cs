namespace BlogSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Data.Entity.Infrastructure;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity.EntityFramework;
    using BlogSystem.Data.Contracts;
    using BlogSystem.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(nameOrConnectionString: "DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Page> Pages { get; set; }

        public IDbSet<BlogPost> Posts { get; set; }

        public IDbSet<PostComment> PostComments { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();

            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            IEnumerable<DbEntityEntry> entryset = this.ChangeTracker.Entries().Where(e => e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in entryset)
            {
                IAuditInfo entity = (IAuditInfo) entry.Entity;

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

        public System.Data.Entity.DbSet<BlogSystem.Data.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}