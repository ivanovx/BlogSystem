namespace BlogSystem.Services.Data.Categories
{
    using System.Linq;
    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    public class CategoriesDataService : ICategoriesDataService
    {
        private readonly IDbRepository<Category> categoriesData;

        public CategoriesDataService(IDbRepository<Category> categoriesData)
        {
            this.categoriesData = categoriesData;
        }

        public IQueryable<Category> GetAllCategories()
        {
            var categories = this.categoriesData
                .All()
                .OrderByDescending(c => c.CreatedOn)
                .AsQueryable();

            return categories;
        }

        public Category GetCategory(int id)
        {
            var category = this.categoriesData.Find(id);

            return category;
        }
    }
}
