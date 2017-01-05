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
            this.AutomaticMigrationDataLossAllowed = true;
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

            context.Settings.AddOrUpdate(
                    s => s.Id,
                    new Setting { Id = "Name", Value = "Blog name" },
                    new Setting { Id = "Description", Value = "Blog description" },
                    new Setting { Id = "Keywords", Value = "Blog keywords" },
                    new Setting { Id = "Author", Value = "Blog author" },
                    new Setting { Id = "Facebook", Value = "Facebook"},
                    new Setting { Id = "Twitter", Value = "Twitter" },
                    new Setting { Id = "LinkedIn", Value = "LinkedIn" },
                    new Setting { Id = "GitHub", Value = "GitHub" },
                    new Setting { Id = "YouTube", Value = "YouTube" },
                    new Setting { Id = "Email", Value = "Email"});
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