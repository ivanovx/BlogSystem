namespace BlogSystem.Services.Data
{
    using System.Linq;
    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;
    using BlogSystem.Services.Data.Contracts;

    public class PagesDataService : IPagesDataService
    {
        private readonly IDbRepository<Page> pages;

        public PagesDataService(IDbRepository<Page> pages)
        {
            this.pages = pages;
        }

        public IQueryable<Page> GetAll()
        {
            return this.pages.All().Where(p => !p.IsDeleted);
        }

        public Page GetPage(string permalink)
        {
            return this.GetAll().FirstOrDefault(p => p.Permalink.ToLower().Trim() == permalink.ToLower().Trim());
        }
    }
}
