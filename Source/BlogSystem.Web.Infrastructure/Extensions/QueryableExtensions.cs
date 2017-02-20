namespace BlogSystem.Web.Infrastructure.Extensions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper.QueryableExtensions;
    using Mapping;

    public static class QueryableExtensions
    {
        public static IQueryable<T> To<T>(this IQueryable source, params Expression<Func<T, object>>[] membersToExpand)
        {
            return source.ProjectTo(AutoMapperConfig.MapperConfiguration, membersToExpand);
        }
    }
}