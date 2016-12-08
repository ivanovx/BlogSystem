namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using ViewModels.Home;

    public class HomeController : AdministrationController
    {
        public HomeController(IBlogSystemData data)
            : base(data)
        {
        }

        // GET: Administration/Home
        public ActionResult Index()
        {
            var blogPosts = this.Data.Posts
                .All()
                .Count();

            var comments = this.Data.PostComments
                .All()
                .Count();

            var applicationUsers = this.Data.Users
                .All()
                .Count();

            var pages = this.Data.Pages
                .All()
                .Count();

            var model = new IndexAdminPageViewModel
            {
                BlogPostsCount = blogPosts,
                CommentsCount = comments,
                ApplicationUsersCount = applicationUsers,
                PagesCount = pages
            };

            return this.View(model);
        }
    }
}