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

            context.Settings.Add(new Setting { Key = "BlogTitle", Value = "Blog Title" });
            context.Settings.Add(new Setting { Key = "BlogDescription", Value = "Blog Description" });
            context.Settings.Add(new Setting { Key = "BlogKeywords", Value = "Blog Keywords" });
            context.Settings.Add(new Setting { Key = "BlogAuthor", Value = "Blog Author" });
            context.Settings.Add(new Setting { Key = "GitHubProfile", Value = "GitHub Profile" });
            context.Settings.Add(new Setting { Key = "LinkedInProfile", Value = "LinkedIn Profile" });
            context.Settings.Add(new Setting { Key = "AuthorEmail", Value = "Author Email" });
            context.Settings.Add(new Setting { Key = "FacebookProfile", Value = "Facebook Profile" });
            context.Settings.Add(new Setting { Key = "TwitterProfile", Value = "Twitter Profile" });
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

            this.userManager.Create(admin, "csyntax11");
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