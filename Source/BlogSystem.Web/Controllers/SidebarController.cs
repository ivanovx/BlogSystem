namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using Data.Repositories;
    using ViewModels.Sidebar;
    using ViewModels.Posts;
    using ViewModels.Pages;
    using Infrastructure.Extensions;
    using Infrastructure.Caching;

    public class SidebarController : BaseController
    {
        private readonly IDbRepository<Post> postsRepository;
        private readonly IDbRepository<Page> pagesRepository;
        private readonly ICacheService cacheService;

        public SidebarController(IDbRepository<Post> postsRepository, IDbRepository<Page> pagesRepository, ICacheService cacheService)
        {
            this.postsRepository = postsRepository;
            this.pagesRepository = pagesRepository;
            this.cacheService = cacheService;
        }

        [ChildActionOnly]
        public PartialViewResult Index()
        {
            var model = new SidebarViewModel
            {
                RecentPosts = this.cacheService.Get("RecentPosts",
                    () =>
                        this.postsRepository
                            .All()
                            .Where(p => !p.IsDeleted)
                            .OrderByDescending(p => p.CreatedOn)
                            .To<PostViewModel>()
                            .Take(5)
                            .ToList(),
                    600),
                AllPages = this.cacheService.Get("AllPages", 
                    () => 
                        this.pagesRepository
                        .All()
                        .To<PageViewModel>()
                        .ToList(), 
                    600)
            };

            return this.PartialView(model);
        }
    }
}