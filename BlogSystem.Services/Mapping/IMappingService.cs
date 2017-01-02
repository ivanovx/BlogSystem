namespace BlogSystem.Services.Mapping
{
    using System.Linq;

    public interface IMappingService : IService
    {
        TDestination Map<TDestination>(object source);

        void Map<TSource, TDestination>(TSource source, TDestination destination);

        IQueryable<TDestination> MapCollection<TDestination>(IQueryable source, object parameters = null);
    }
}
