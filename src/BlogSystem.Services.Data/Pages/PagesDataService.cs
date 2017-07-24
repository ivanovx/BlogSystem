namespace BlogSystem.Services.Data.Pages
{
    using System.Linq;
    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    public class PagesDataService : IPagesDataService
    {
        private readonly IDbRepository<Page> pagesData;

        public PagesDataService(IDbRepository<Page> pagesData)
        {
            this.pagesData = pagesData;
        }

        public IQueryable<Page> GetAllPages()
        {
            var pages = this.pagesData
                .All()
                .Where(p => !p.IsDeleted)
                .AsQueryable();

            return pages;
        }

        public IQueryable<Page> GetAllPagesForMenu()
        {
            var pages = this.GetAllPages().Where(p => p.ShowInMenu == true).AsQueryable();

            return pages;
        }

        public Page GetPage(string permalink)
        {
            var page = this.GetAllPages().FirstOrDefault(p => p.Permalink.ToLower().Trim() == permalink.ToLower().Trim());

            return page;
        }
    }
}
