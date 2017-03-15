namespace BlogSystem.Services.Data.Pages
{
    using System.Linq;
    using BlogSystem.Data.Models;

    public interface IPagesDataService
    {
        IQueryable<Page> GetAll();

        IQueryable<Page> GetAllForMenu();

        Page GetPage(string permalink);
    }
}