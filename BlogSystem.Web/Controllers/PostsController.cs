namespace BlogSystem.Web.Controllers
{ 
    using System.Web.Mvc;
    using Data.UnitOfWork;
    using ViewModels.Blog;

    public class PostsController : BaseController
    {
        private readonly IBlogSystemData data;

        public PostsController(IBlogSystemData data)
        {
            this.data = data;
        }

        public ActionResult Post(int id)
        {
            var post = this.data.Posts.Find(id);
            var model = this.Mapper.Map<PostViewModel>(post);

            return this.View(model);
        }
    }
}