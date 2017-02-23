using System.Linq;

namespace BlogSystem.Web.Infrastructure.Mapping.Service
{
    public interface IMappingService
    {
        TDestination Map<TDestination>(object source);

        void Map<TSource, TDestination>(TSource source, TDestination destination);

        IQueryable<TDestination> MapCollection<TDestination>(IQueryable source, object parameters = null);
    }
}