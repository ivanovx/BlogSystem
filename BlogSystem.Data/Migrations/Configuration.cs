namespace BlogSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
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

            context.Settings.Add(new Setting
            {
                Key = "Logo URL",
                Value = "/img/header-logo.png"
            });

            context.Settings.Add(new Setting
            {
                Key = "Author",
                Value = "Author"
            });

            context.Settings.Add(new Setting
            {
                Key = "GitHub Profile",
                Value = "GitHub Profile"
            });

            context.Settings.Add(new Setting
            {
                Key = "LinkedIn Profile",
                Value = "LinkedIn Profile"
            });

            context.Settings.Add(new Setting
            {
                Key = "Twitter Profile",
                Value = "Twitter Profile"
            });

            context.Settings.Add(new Setting
            {
                Key = "Facebook Profile",
                Value = "Facebook Profile"
            });
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