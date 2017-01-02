namespace BlogSystem.Services.Mapping
{
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class AutoMapperMappingService : IMappingService
    {
        private readonly IMapper mapper;

        public AutoMapperMappingService(IMapper mapper)
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
