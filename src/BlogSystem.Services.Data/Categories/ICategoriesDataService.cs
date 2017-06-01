namespace BlogSystem.Services.Data.Categories
{
    using System.Linq;

    using BlogSystem.Data.Models;

    public interface ICategoriesDataService : IDataService
    {
        IQueryable<Category> GetAllCategories();

        Category GetCategory(int id);
    }
}