namespace BlogSystem.Web.Infrastructure.Identity
{
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Data.Models;
    using Data.Repositories;

    public class CurrentUser : ICurrentUser
    {
        private readonly IDbRepository<ApplicationUser> usersRepository;

        public CurrentUser(IDbRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public ApplicationUser Get()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();

            return this.usersRepository.Find(userId);
        }
    }
}