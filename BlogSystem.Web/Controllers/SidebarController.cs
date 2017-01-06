using BlogSystem.Data.Models;
using BlogSystem.Data.Repositories;

namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using ViewModels.Sidebar;
    using ViewModels.Blog;
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
        [OutputCache(Duration = 10 * 60)]
        public PartialViewResult Index()
        {
            var model = new SidebarViewModel
            {
                RecentPosts = this.Cache.Get("RecentBlogPosts",
                    () =>
                        this.postsRepository
                            .All()
                            .Where(p => !p.IsDeleted)
                            .OrderByDescending(p => p.CreatedOn)
                            .To<PostViewModel>()
                            .Take(5)
                            .ToList(),
                    600),
                Pages = this.pagesRepository.All().ToList()
            };

            return this.PartialView(model);
        }
    }
}