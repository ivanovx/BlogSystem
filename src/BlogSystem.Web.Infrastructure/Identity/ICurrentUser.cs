namespace BlogSystem.Web.Infrastructure.Identity
{
    using Data.Models;

    public interface ICurrentUser
    {
        ApplicationUser GetUser();
    }
}