using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BlogSystem.Data.Models;
using BlogSystem.Web.Infrastructure.Mapping;
using BlogSystem.Web.ViewModels.Home;

namespace BlogSystem.Web.ViewModels
{
    public class CategoryViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public int PostsCont { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<Category, CategoryViewModel>()
                .ForMember(m => m.PostsCont, c => c.MapFrom(x => x.Posts.Count));
        }
    }
}