namespace BlogSystem.Web.Infrastructure.Mapping
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using AutoMapper;

    public static class AutoMapperConfig
    {
        public static IConfigurationProvider MapperConfiguration { get; private set; }

        public static void Execute(Assembly assembly)
        {
            var types = assembly.GetExportedTypes();

            MapperConfiguration = new MapperConfiguration(config =>
            {
                RegisterStandardFromMappings(config, types);
                RegisterStandardToMappings(config, types);
                RegisterCustomMaps(config, types);
            });
        }

        private static void RegisterStandardFromMappings(IProfileExpression config, IEnumerable<Type> types)
        {
            var maps = GetFromMaps(types);

            CreateMappings(config, maps);
        }

        private static void RegisterStandardToMappings(IProfileExpression config, IEnumerable<Type> types)
        {
            var maps = GetToMaps(types);

            CreateMappings(config, maps);
        }

        private static void RegisterCustomMaps(IMapperConfigurationExpression config, IEnumerable<Type> types)
        {
            var maps = GetCustomMappings(types);

            CreateMappings(config, maps);
        }

        private static IEnumerable<IHaveCustomMappings> GetCustomMappings(IEnumerable<Type> types)
        {
            var customMaps = from t in types
                             from i in t.GetInterfaces()
                             where typeof(IHaveCustomMappings).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface
                             select (IHaveCustomMappings) Activator.CreateInstance(t);

            return customMaps;
        }

        private static IEnumerable<TypeMap> GetFromMaps(IEnumerable<Type> types)
        {
            var fromMaps = from t in types
                           from i in t.GetInterfaces()
                           where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) 
                           && !t.IsAbstract && !t.IsInterface
                           select new TypeMap
                           {
                               Source = i.GetGenericArguments().First(),
                               Destination = t
                           };

            return fromMaps;
        }

        private static IEnumerable<TypeMap> GetToMaps(IEnumerable<Type> types)
        {
            var toMaps = from t in types
                         from i in t.GetInterfaces()
                         where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>) 
                         && !t.IsAbstract && !t.IsInterface
                         select new TypeMap
                         {
                             Source = t,
                             Destination = i.GetGenericArguments().First()
                         };

            return toMaps;
        }

        private static void CreateMappings(IProfileExpression config, IEnumerable<TypeMap> maps)
        {
            foreach (var map in maps)
            {
                config.CreateMap(map.Source, map.Destination);
            }
        }

        private static void CreateMappings(IMapperConfigurationExpression config, IEnumerable<IHaveCustomMappings> maps)
        {
            foreach (var map in maps)
            {
                map.CreateMappings(config);
            }
        }
    }
}