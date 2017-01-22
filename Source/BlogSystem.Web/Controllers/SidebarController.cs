namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using Data.Repositories;
    using ViewModels.Sidebar;
    using ViewModels.Post;
    using ViewModels.Page;
    using Infrastructure.Extensions;

    public class SidebarController : BaseController
    {
        private readonly IDbRepository<Post> postsRepository;
        private readonly IDbRepository<Page> pagesRepository;

        public SidebarController(IDbRepository<Post> postsRepository, IDbRepository<Page> pagesRepository)
        {
            this.postsRepository = postsRepository;
            this.pagesRepository = pagesRepository;
        }

        [ChildActionOnly]
        public PartialViewResult Index()
        {
            var model = new SidebarViewModel
            {
                RecentPosts = this.Cache.Get("RecentPosts",
                    () =>
                        this.postsRepository
                            .All()
                            .Where(p => !p.IsDeleted)
                            .OrderByDescending(p => p.CreatedOn)
                            .To<PostViewModel>()
                            .Take(5)
                            .ToList(),
                    600),
                Pages = this.Cache.Get("AllPages", 
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