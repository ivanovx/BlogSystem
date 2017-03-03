using BlogSystem.Data.Models;
using BlogSystem.Web.Infrastructure.Mapping;

namespace BlogSystem.Web.Areas.Administration.ViewModels
{
    public class CategoryViewModel: IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}