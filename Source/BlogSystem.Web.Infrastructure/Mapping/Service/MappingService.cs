using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace BlogSystem.Web.Infrastructure.Mapping.Service
{
    public class MappingService : IMappingService
    {
        private readonly IMapper mapper;

        public MappingService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TDestination Map<TDestination>(object source) => this.mapper.Map<TDestination>(source);

        public void Map<TSource, TDestination>(TSource source, TDestination destination) =>
            this.mapper.Map(source, destination);

        public IQueryable<TDestination> MapCollection<TDestination>(IQueryable source, object parameters = null) =>
            source.ProjectTo<TDestination>(this.mapper.ConfigurationProvider, parameters);
    }
}
