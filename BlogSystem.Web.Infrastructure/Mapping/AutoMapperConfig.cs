namespace BlogSystem.Web.Infrastructure.Mapping
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using AutoMapper;

    public static class AutoMapperConfig
    {
        public static MapperConfiguration Configuration { get; private set; }

        public static void Execute(Assembly assembly)
        {
            Configuration = new MapperConfiguration(cfg => 
            {
                var types = assembly.GetExportedTypes();

                LoadStandardMappings(cfg, types);
                LoadReverseMappings(cfg, types);
                LoadCustomMappings(cfg, types);
            });
        }

        private static void LoadStandardMappings(IMapperConfigurationExpression config, IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) && !t.IsAbstract && !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments().First(),
                            Destination = t
                        }).ToArray();

            foreach (var map in maps)
            {
                config.CreateMap(map.Source, map.Destination);
            }
        }

        private static void LoadReverseMappings(IMapperConfigurationExpression config, IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>) && !t.IsAbstract && !t.IsInterface
                        select new
                        {
                            Destination = i.GetGenericArguments().First(),
                            Source = t
                        }).ToArray();

            foreach (var map in maps)
            {
                config.CreateMap(map.Source, map.Destination);
            }
        }

        private static void LoadCustomMappings(IMapperConfigurationExpression config, IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(IHaveCustomMappings).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface
                        select (IHaveCustomMappings) Activator.CreateInstance(t, true)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(config);
            }
        }
    }
}