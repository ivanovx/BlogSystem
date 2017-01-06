using BlogSystem.Data.Repositories;

namespace BlogSystem.Web.Infrastructure.Identity
{
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Data.Models;

    public class CurrentUser : ICurrentUser
    {
        //private readonly IBlogSystemData data;
        private readonly IDbRepository<ApplicationUser> usersRepository;

        public CurrentUser(IDbRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public ApplicationUser Get()
        {
            string userId = HttpContext.Current.User.Identity.GetUserId();

            return this.usersRepository.Find(userId);
        }
    }
}