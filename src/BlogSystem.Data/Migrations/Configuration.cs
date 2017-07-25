namespace BlogSystem.Data.Migrations
{
    using System;
    using System.Linq;
    using System.Data.Entity.Migrations;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using BlogSystem.Common;
    using BlogSystem.Data.Models;

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

            context.Settings.Add(new Setting { Key = "Blog_Title", Value = "Blog Title" });
            context.Settings.Add(new Setting { Key = "Blog_Logo", Value = "Blog Logo" });
            context.Settings.Add(new Setting { Key = "Blog_Description", Value = "Blog Description" });
            context.Settings.Add(new Setting { Key = "Blog_Keywords", Value = "Blog Keywords" });
            context.Settings.Add(new Setting { Key = "Blog_Author", Value = "Blog Author" });
            context.Settings.Add(new Setting { Key = "Author_Email", Value = "Author Email" });
            context.Settings.Add(new Setting { Key = "Facebook_Profile", Value = "Facebook Profile" });
            context.Settings.Add(new Setting { Key = "Twitter_Profile", Value = "Twitter Profile" });
            context.Settings.Add(new Setting { Key = "GitHub_Profile", Value = "GitHub Profile" });
            context.Settings.Add(new Setting { Key = "Instagram_Profile", Value = "Instagram Profile" });
            context.Settings.Add(new Setting { Key = "YouTube_Profile", Value = "YouTube Profile" });
            context.Settings.Add(new Setting { Key = "Spotify_Profile", Value = "Spotify Profile" });
        }

        private void SeedAdmin(ApplicationDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var admin = new ApplicationUser
            {
                Email = "csyntax@outlook.com",
                UserName = "csyntax",
                CreatedOn = DateTime.Now
            };

            this.userManager.Create(admin, "csyntax");
            this.userManager.AddToRole(admin.Id, GlobalConstants.AdministratorRoleName);
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(
                u => u.Name, 
                new IdentityRole(GlobalConstants.AdministratorRoleName),
                new IdentityRole(GlobalConstants.UserRoleName)
            );

            context.SaveChanges();
        }
    }
}