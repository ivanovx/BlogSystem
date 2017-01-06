namespace BlogSystem.Data.Migrations
{
    using System;
    using System.Linq;
    using System.Data.Entity.Migrations;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Common;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private UserManager<ApplicationUser> userManager;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            this.SeedSettings(context);
            this.SeedRoles(context);
            this.SeedAdmin(context);
        }

        private void SeedSettings(ApplicationDbContext context)
        {
            if (context.Settings.Any())
            {
                return;
            }

            context.Settings.Add(new Setting { Key = "Title", Value = "Blog Title" });
            context.Settings.Add(new Setting { Key = "Description", Value = "Blog Description" });
            context.Settings.Add(new Setting { Key = "Keywords", Value = "Blog Keywords" });
            context.Settings.Add(new Setting { Key = "Author", Value = "Blog Author" });
            context.Settings.Add(new Setting { Key = "GitHub", Value = "GitHub Profile" });
            context.Settings.Add(new Setting { Key = "LinkedIn", Value = "LinkedIn Profile" });
            context.Settings.Add(new Setting { Key = "Email", Value = "Author Email" });
            context.Settings.Add(new Setting { Key = "Facebook", Value = "Facebook Profile" });
            context.Settings.Add(new Setting { Key = "Twitter", Value = "Twitter Profile" });
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
            context.Roles.AddOrUpdate(u => u.Name, new IdentityRole(GlobalConstants.AdminRoleName));
            context.SaveChanges();
        }
    }
}