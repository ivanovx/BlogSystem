namespace BlogSystem.Services.Web.Mapping
{
    using System.Linq;

    public interface IMappingService : IWebService
    {
        TDestination Map<TDestination>(object source);

        void Map<TSource, TDestination>(TSource source, TDestination destination);

        IQueryable<TDestination> Map<TDestination>(IQueryable source, object parameters = null);
    }
}