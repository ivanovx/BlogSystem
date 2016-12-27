namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using ViewModels.Home;

    public class HomeController : AdministrationController
    {
        private readonly IBlogSystemData data;
        public HomeController(IBlogSystemData data)
        {
            this.data = data;
        }

        // GET: Administration/Home
        public ActionResult Index()
        {
            int postsCount = this.data.Posts.All().Count();
            int commentsCount = this.data.Comments.All().Count();
            int usersCount = this.data.Users.All().Count();
            int pagesCount = this.data.Pages.All().Count();

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