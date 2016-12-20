namespace BlogSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Common;
    using Data.UnitOfWork;
    using Infrastructure.Mapping;
    using ViewModels.Home;

    public class HomeController : BaseController
    {
        public HomeController(IBlogSystemData data)
        {
            this.Data = data;
        }

        public IBlogSystemData Data { get; }

        public ActionResult Index(int page = 1, int perPage = GlobalConstants.DefaultPageSize)
        {
            int pagesCount = (int) Math.Ceiling(this.Data.Posts.All().Count() / (decimal) perPage);

            var posts = this.Data.Posts
                .All()
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.CreatedOn)
                .To<BlogPostConciseViewModel>()
                .Skip(perPage * (page - 1))
                .Take(perPage);
          
            var model = new IndexPageViewModel
            {
                Posts = posts.ToList(),
                CurrentPage = page,
                PagesCount = pagesCount
            };

            return this.View(model);
        }
    }
}