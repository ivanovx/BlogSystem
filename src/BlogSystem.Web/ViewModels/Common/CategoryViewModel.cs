namespace BlogSystem.Web.ViewModels.Common
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CategoryViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PostsCont { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<Category, CategoryViewModel>()
                .ForMember(c => c.PostsCont, c => c.MapFrom(category => category.Posts.Count));
        }
    }
}