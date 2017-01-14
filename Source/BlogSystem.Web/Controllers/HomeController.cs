namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Common;
    using Data.Models;
    using Data.Repositories;
    using Infrastructure.Extensions;
    using ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IDbRepository<Post> postsRepository;

        public HomeController(IDbRepository<Post> postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public ActionResult Index(int page = 1, int perPage = GlobalConstants.DefaultPageSize)
        {
            int pagesCount = (int) Math.Ceiling(this.postsRepository.All().Count() / (decimal) perPage);

            var posts = this.postsRepository.All().Where(p => !p.IsDeleted).OrderByDescending(p => p.CreatedOn).To<PostConciseViewModel>().Skip(perPage * (page - 1)).Take(perPage).ToList();

            var model = new IndexPageViewModel
            {
                Posts = posts,
                CurrentPage = page,
                PagesCount = pagesCount
            };

            return this.View(model);
        }
    }
}