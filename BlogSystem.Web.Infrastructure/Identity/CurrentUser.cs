namespace BlogSystem.Web.Infrastructure.Identity
{
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Data.Models;
    using Data.UnitOfWork;

    public class CurrentUser : ICurrentUser
    {
        private readonly IBlogSystemData data;

        public CurrentUser(IBlogSystemData data)
        {
            this.data = data;
        }

        public ApplicationUser Get()
        {
            string userId = HttpContext.Current.User.Identity.GetUserId();

            return this.data.Users.Find(userId);
        }
    }
}