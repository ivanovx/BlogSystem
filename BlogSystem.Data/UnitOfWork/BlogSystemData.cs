namespace BlogSystem.Data.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class BlogSystemData : IBlogSystemData
    {
        private readonly DbContext dbContext;

        private readonly IDictionary<Type, object> repositories;

        private IUserStore<ApplicationUser> userStore;

        public BlogSystemData(DbContext context)
        {
            this.dbContext = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IUserStore<ApplicationUser> UserStore
            => this.userStore ?? (this.userStore = new UserStore<ApplicationUser>(this.dbContext));

        public IRepository<ApplicationUser> Users => this.GetRepository<ApplicationUser>();

        public IRepository<BlogPost> Posts => this.GetRepository<BlogPost>();

        public IRepository<PostComment> PostComments => this.GetRepository<PostComment>();

        public int SaveChanges()
        {
             return this.dbContext.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericEfRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.dbContext));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}