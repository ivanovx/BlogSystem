namespace BlogSystem.Data.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Repositories;

    // Todo
    public class BlogSystemData : IBlogSystemData
    {
        private readonly DbContext context;
        private readonly IDictionary<Type, object> repositories;

        private IUserStore<ApplicationUser> userStore;

        public BlogSystemData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        // Todo
        public IUserStore<ApplicationUser> UserStore
        {
            get
            {
                return this.userStore ?? (this.userStore = new UserStore<ApplicationUser>(this.context));
            }
        }

        public IDbRepository<ApplicationUser> Users
        {
            get
            {
                return this.GetRepository<ApplicationUser>();
            }
        }

        public IDbRepository<Post> Posts
        {
            get
            {
                return this.GetRepository<Post>();
            }
        }

        public IDbRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
            }
        }

        public IDbRepository<Page> Pages
        {
            get
            {
                return this.GetRepository<Page>();
            }
        } 

        public IDbRepository<Setting> Settings
        {
            get
            {
                return this.GetRepository<Setting>();
            }
        }


        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IDbRepository<T> GetRepository<T>() 
            where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                this.repositories.Add(typeof(T), Activator.CreateInstance(typeof(DbRepository<T>), this.context));
            }

            return (IDbRepository<T>) this.repositories[typeof(T)];
        }
    }
}