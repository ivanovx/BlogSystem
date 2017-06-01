using BlogSystem.Data.Models;
using BlogSystem.Web.Infrastructure.Mapping;
using System.Collections.Generic;

namespace BlogSystem.Web.ViewModels.Posts
{
    public class PostsByCategoryViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public IEnumerable<PostViewModel> Posts { get; set; }
    }
}