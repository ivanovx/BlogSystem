using BlogSystem.Web.Infrastructure.Mapping.Service;

namespace BlogSystem.Web.Controllers
{ 
    using System.Web.Mvc;
    using Data.Models;
    using Data.Repositories;
    using ViewModels.Posts;
    using Infrastructure;

    public class PostsController : BaseController
    {
        private readonly IDbRepository<Post> postsRepository;
        private readonly IMappingService mappingService;

        public PostsController(IDbRepository<Post> postsRepository, IMappingService mappingService)
        {
            this.postsRepository = postsRepository;
            this.mappingService = mappingService;
        }

        public ActionResult Post(int year, int month, string urlTitle, int id)
        {
            this.ViewData["id"] = id;

            var post = this.postsRepository.Find(id);
            var model = this.mappingService.Map<PostViewModel>(post);

            return this.View(model);
        }
    }
}