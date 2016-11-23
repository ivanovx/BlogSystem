namespace BlogSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using BlogSystem.Data.Contracts;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser, IAuditInfo
    {
        private ICollection<PostComment> comments;

        private ICollection<BlogPost> posts;

        private ICollection<Page> pages;

        public ApplicationUser()
        {
            this.posts = new HashSet<BlogPost>();
            this.comments = new HashSet<PostComment>();
            this.pages = new HashSet<Page>();
        }

        public virtual ICollection<BlogPost> Posts
        {
            get
            {
                return this.posts;
            }

            set
            {
                this.posts = value;
            }
        }

        public virtual ICollection<PostComment> Comments
        {
            get
            {
                return this.comments;
            }

            set
            {
                this.comments = value;
            }
        }

        public virtual ICollection<Page> Pages
        {
            get
            {
                return this.pages;
            }

            set
            {
                this.pages = value;
            }
        }

        public DateTime CreatedOn { get; set; }

        [NotMapped]
        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}