namespace BlogSystem.Web.ViewModels.Page
{
    using System;
    using AutoMapper;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class PageViewModel : IMapFrom<Data.Models.Page>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime LastModified { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Data.Models.Page, PageViewModel>()
                .ForMember(model => model.LastModified, config => config.MapFrom(page => page.ModifiedOn ?? page.CreatedOn));
        }
    }
}