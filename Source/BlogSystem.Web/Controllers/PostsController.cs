namespace BlogSystem.Web.Controllers
{ 
    using System.Web.Mvc;
    using ViewModels.Posts;
    using BlogSystem.Services.Data.Contracts;
    using BlogSystem.Services.Web.Mapping;

    public class PostsController : BaseController
    {
        private readonly IPostsDataService postsData;
        private readonly IMappingService mappingService;

        public PostsController(IPostsDataService postsData, IMappingService mappingService)
        {
            this.postsData = postsData;
            this.mappingService = mappingService;
        }

        public ActionResult Post(int year, int month, string urlTitle, int id)
        {
            this.ViewData["id"] = id;

            var post = this.postsData.GetPost(id);
            var model = this.mappingService.Map<PostViewModel>(post);

            return this.View(model);
        }
    }
}