namespace BlogSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using BlogSystem.Web.ViewModels.Posts;
    using BlogSystem.Services.Data.Categories;

    using BlogSystem.Services.Data.Posts;
    using BlogSystem.Services.Web.Mapping;
   
    public class PostsController : BaseController
    {
        private readonly IPostsDataService postsData;
        private readonly ICategoriesDataService categoriesData;

        public PostsController(IPostsDataService postsData, ICategoriesDataService categoriesData)
        {
            this.postsData = postsData;
            this.categoriesData = categoriesData;
        }

        public ActionResult Post(int year, int month, string urlTitle, int id)
        {
            this.ViewData["id"] = id;

            var post = this.postsData.GetPost(id);

            if (post == null)
            {
                return this.HttpNotFound();
            }

            var model = this.mapping.Map<PostViewModel>(post);

            return this.View(model);
        }

        public ActionResult PostsByCategory(int id)
        {
            var category = this.categoriesData.GetCategory(id);
            var postsByCategory = this.postsData.GetAllPostsByCategory(id);

            var posts = this.mapping.Map<PostViewModel>(postsByCategory).ToList();

            var model = new PostsByCategoryViewModel
            {
                Name = category.Name,
                Posts = posts
            };

            return this.View(model);
        }
    }

}