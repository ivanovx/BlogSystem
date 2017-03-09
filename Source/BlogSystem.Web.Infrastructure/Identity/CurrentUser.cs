namespace BlogSystem.Web.Infrastructure.Identity
{
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Data.Models;
    using Data.Repositories;

    public class CurrentUser : ICurrentUser
    {
        private readonly IDbRepository<ApplicationUser> users;

        public CurrentUser(IDbRepository<ApplicationUser> users)
        {
            this.users = users;
        }

        public ApplicationUser GetUser
        {
            get
            {
                var userId = HttpContext.Current.User.Identity.GetUserId();

                return this.users.Find(userId);
            }
        }
    }
}