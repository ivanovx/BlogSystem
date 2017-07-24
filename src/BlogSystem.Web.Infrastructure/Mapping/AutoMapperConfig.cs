namespace BlogSystem.Web.Infrastructure.Mapping
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;

    using AutoMapper;

    public class AutoMapperConfig
    {
        public static IConfigurationProvider MapperConfiguration { get; private set; }

        public static void RegisterMappings(Assembly assembly)
        {
            var types = assembly.GetExportedTypes();

            MapperConfiguration = new MapperConfiguration(configuration =>
            {
                RegisterStandardFromMappings(configuration, types);

                RegisterStandardToMappings(configuration, types);

                RegisterCustomMaps(configuration, types);
            });

            // TODO: MapperConfiguration.AssertConfigurationIsValid();
        }

        private static void RegisterStandardFromMappings(IProfileExpression configuration, IEnumerable<Type> types)
        {
            var maps = GetFromMaps(types);

            CreateMappings(configuration, maps);
        }

        private static void RegisterStandardToMappings(IProfileExpression configuration, IEnumerable<Type> types)
        {
            var maps = GetToMaps(types);

            CreateMappings(configuration, maps);
        }

        private static void RegisterCustomMaps(IMapperConfigurationExpression configuration, IEnumerable<Type> types)
        {
            var maps = GetCustomMappings(types);

            CreateMappings(configuration, maps);
        }

        private static IEnumerable<IHaveCustomMappings> GetCustomMappings(IEnumerable<Type> types)
        {
            var customMaps = from t in types
                             from i in t.GetInterfaces()
                             where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
                                !t.IsAbstract &&
                                !t.IsInterface
                             select (IHaveCustomMappings)Activator.CreateInstance(t);

            return customMaps;
        }

        private static IEnumerable<TypeMap> GetFromMaps(IEnumerable<Type> types)
        {
            var fromMaps = from t in types
                           from i in t.GetInterfaces()
                           where i.IsGenericType &&
                                 i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                                 !t.IsAbstract &&
                                 !t.IsInterface
                           select new TypeMap
                           {
                               Source = i.GetGenericArguments()[0],
                               Destination = t
                           };

            return fromMaps;
        }

        private static IEnumerable<TypeMap> GetToMaps(IEnumerable<Type> types)
        {
            var toMaps = from t in types
                         from i in t.GetInterfaces()
                         where i.IsGenericType &&
                               i.GetGenericTypeDefinition() == typeof(IMapTo<>) &&
                               !t.IsAbstract &&
                               !t.IsInterface
                         select new TypeMap
                         {
                             Source = t,
                             Destination = i.GetGenericArguments()[0]
                         };

            return toMaps;
        }

        private static void CreateMappings(IProfileExpression configuration, IEnumerable<TypeMap> maps)
        {
            foreach (var map in maps)
            {
                configuration.CreateMap(map.Source, map.Destination);
            }
        }

        private static void CreateMappings(
            IMapperConfigurationExpression configuration,
            IEnumerable<IHaveCustomMappings> maps)
        {
            foreach (var map in maps)
            {
                map.CreateMappings(configuration);
            }
        }
    }
}