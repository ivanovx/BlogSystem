namespace BlogSystem.Services.Data.Pages
{
    using System.Linq;
    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    public class PagesDataService : IPagesDataService
    {
        private readonly IDbRepository<Page> pages;

        public PagesDataService(IDbRepository<Page> pages)
        {
            this.pages = pages;
        }

        public IQueryable<Page> GetAll()
        {
            var pages = this.pages
                .All()
                .Where(p => !p.IsDeleted);

            return pages;
        }

        public IQueryable<Page> GetAllForMenu()
        {
            var pages = this.pages
                .All()
                .Where(p => !p.IsDeleted && p.VisibleInMenu);

            return pages;
        }

        public Page GetPage(string permalink)
        {
            var page = this.pages
                .All()
                .FirstOrDefault(p => !p.IsDeleted && p.Permalink.ToLower().Trim() == permalink.ToLower().Trim());

            return page;
        }
    }
}
