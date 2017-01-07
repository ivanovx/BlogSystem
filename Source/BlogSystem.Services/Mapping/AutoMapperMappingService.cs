namespace BlogSystem.Services.Mapping
{
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Web.Infrastructure.Mapping;

    public class AutoMapperMappingService : IMappingService
    {
        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.MapperConfiguration.CreateMapper();
            }
        }

        public TDestination Map<TDestination>(object source) => Mapper.Map<TDestination>(source);

        public void Map<TSource, TDestination>(TSource source, TDestination destination) =>
            Mapper.Map(source, destination);

        public IQueryable<TDestination> MapCollection<TDestination>(IQueryable source, object parameters = null) =>
            source.ProjectTo<TDestination>(Mapper.ConfigurationProvider, parameters);
    }
}