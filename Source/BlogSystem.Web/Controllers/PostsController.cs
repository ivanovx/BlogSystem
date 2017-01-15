namespace BlogSystem.Web.Controllers
{ 
    using System.Web.Mvc;
    using Data.Models;
    using Data.Repositories;
    using ViewModels.Post;
    using Infrastructure;

    public class PostsController : BaseController
    {
        private readonly IDbRepository<Post> postsRepository;

        public PostsController(IDbRepository<Post> postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        [PassRouteValuesToViewData]
        public ActionResult Post(int year, int month, string urlTitle, int id)
        {
            var post = this.postsRepository.Find(id);
            var model = this.Mapper.Map<PostViewModel>(post);

            return this.View(model);
        }
    }
}