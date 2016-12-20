namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using ViewModels.Home;

    public class HomeController : AdministrationController
    {
        public HomeController(IBlogSystemData data)
        {
            this.Data = data;
        }

        public IBlogSystemData Data { get; }

        // GET: Administration/Home
        public ActionResult Index()
        {
            int postsCount = this.Data.Posts.All().Count();

            int commentsCount = this.Data.Comments.All().Count();

            int usersCount = this.Data.Users
                .All()
                .Count();

            int pagesCount = this.Data.Pages
                .All()
                .Count();

            var model = new IndexAdminPageViewModel
            {
                PostsCount = postsCount,
                CommentsCount = commentsCount,
                PagesCount = pagesCount,
                UsersCount = usersCount
            };

            return this.View(model);
        }
    }
}