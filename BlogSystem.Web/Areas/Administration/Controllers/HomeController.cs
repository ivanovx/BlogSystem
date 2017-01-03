namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using Data.Repositories;
    using ViewModels.Home;
    using Base;

    public class HomeController : AdministrationController
    {
        private readonly IDbRepository<Post> postsRepository;
        private readonly IDbRepository<Comment> commentsRepository;
        private readonly IDbRepository<Page> pagesRepository;
        private readonly IDbRepository<ApplicationUser> usersRepository;

        public HomeController(IDbRepository<Post> postsRepository, IDbRepository<Comment> commentsRepository, IDbRepository<Page> pagesRepository, IDbRepository<ApplicationUser> usersRepository)
        {
            this.postsRepository = postsRepository;
            this.commentsRepository = commentsRepository;
            this.pagesRepository = pagesRepository;
            this.usersRepository = usersRepository;
        }

        // GET: Administration/Home
        public ActionResult Index()
        {
            int postsCount = this.postsRepository.All().Count();
            int commentsCount = this.commentsRepository.All().Count();
            int usersCount = this.usersRepository.All().Count();
            int pagesCount = this.pagesRepository.All().Count();

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