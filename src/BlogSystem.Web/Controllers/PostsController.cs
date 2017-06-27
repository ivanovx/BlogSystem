namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using BlogSystem.Web.ViewModels.Posts;

    using BlogSystem.Services.Data.Posts;

    public class PostsController : BaseController
    {
        private readonly IPostsDataService postsData;

        public PostsController(IPostsDataService postsData)
        {
            this.postsData = postsData;
        }

        public ActionResult Post(int year, int month, string urlTitle, int id)
        {
            this.ViewData["id"] = id;

            var post = this.postsData.GetPost(id);

            if (post == null)
            {
                return this.HttpNotFound();
            }

            var model = this.Mapper.Map<PostViewModel>(post);

            return this.View(model);
        }
    }
}