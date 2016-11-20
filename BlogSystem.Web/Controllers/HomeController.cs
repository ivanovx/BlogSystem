using System;
using BlogSystem.Data.Models;
using BlogSystem.Data.Repositories;

namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using BlogSystem.Web.Infrastructure.Mapping;

    using BlogSystem.Data.UnitOfWork;
    using BlogSystem.Web.ViewModels.Home;

    using BlogSystem.Common;

    using PagedList;

    public class HomeController : BaseController
    {
        public HomeController(IBlogSystemData data) 
            : base(data)
        {
        }

        public ActionResult Index(int page = 1, int perPage = GlobalConstants.PostsPerPageDefaultValue)
        {
            //var pageNumber = page ?? 1;

            var pagesCount = (int) Math.Ceiling(this.Data.Posts.All().Count() / (decimal) perPage);

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
                PagesCount = pagesCount,
            };
            //var model = viewModel.Posts.ToPagedList(pageNumber, GlobalConstants.PostsPerPageDefaultValue);

            return this.View(model);
        }
    }
}