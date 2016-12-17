using System.Web;
using BlogSystem.Data.UnitOfWork;
using Microsoft.AspNet.Identity;

namespace BlogSystem.Web.Identity.User

{
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Data.Models;
    using Data.Repositories;

    public class CurrentUser : ICurrentUser
    {
        private IBlogSystemData data;

        public CurrentUser(IBlogSystemData data)
        {
            this.data = data;
        }

        public ApplicationUser Get()
        {
            string user = HttpContext.Current.User.Identity.GetUserId();

            return this.data.Users.Find(user);
        }
    }
}