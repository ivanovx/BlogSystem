namespace BlogSystem.Web.Infrastructure.Identity
{
    using System.Web;
    using Microsoft.AspNet.Identity;

    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    public class CurrentUser : ICurrentUser
    {
        private readonly IDbRepository<ApplicationUser> usersData;

        public CurrentUser(IDbRepository<ApplicationUser> usersData)
        {
            this.usersData = usersData;
        }

        public ApplicationUser GetUser()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = this.usersData.Find(userId);

            return user;
        }
    }
}