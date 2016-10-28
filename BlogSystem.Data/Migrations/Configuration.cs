using BlogSystem.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BlogSystem.Data.Migrations
{
    using BlogSystem.Common;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogSystem.Data.ApplicationDbContext>
    {
        private UserManager<ApplicationUser> userManager;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BlogSystem.Data.ApplicationDbContext";

            // TODO: Remove in production
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            this.SeedRoles(context);
            this.SeedAdmin(context);
        }

        private void SeedAdmin(ApplicationDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var admin = new ApplicationUser
            {
                Email = "admin@mysite.com",
                UserName = "Administrator",
                CreatedOn = DateTime.Now
            };
            this.userManager.Create(admin, "admin123456");
            this.userManager.AddToRole(admin.Id, GlobalConstants.AdminRoleName);
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole(GlobalConstants.AdminRoleName));
            context.SaveChanges();
        }
    }
}
