using BlogSystem.Services.Identity;

namespace BlogSystem.Web.Identity.User
{
    using Data.Models;

    public interface ICurrentUser : IService
    {
        ApplicationUser Get();
    }
}