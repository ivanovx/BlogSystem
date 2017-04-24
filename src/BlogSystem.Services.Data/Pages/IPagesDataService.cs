namespace BlogSystem.Services.Data.Pages
{
    using System.Linq;
    using BlogSystem.Data.Models;

    public interface IPagesDataService
    {
        IQueryable<Page> GetAllPages();

        IQueryable<Page> GetAllPagesForMenu();

        Page GetPage(string permalink);
    }
}