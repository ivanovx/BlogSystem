namespace BlogSystem.Web.Infrastructure.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AutoMapper;

    public static class AutoMapperConfig
    {
        public static MapperConfiguration Configuration { get; private set; }

        public static void Execute(Assembly assembly)
        {
            Configuration = new MapperConfiguration(cfg =>
            {
                var types = assembly.GetExportedTypes();

                LoadStandardMappings(types, cfg);
                //LoadReverseMappings(types, cfg);
                LoadCustomMappings(types, cfg);
            });
        }

        private static void LoadStandardMappings(IEnumerable<Type> types, IProfileExpression mapperConfiguration)
        {
            /*var maps = (from t in types
                from i in t.GetInterfaces()
                where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) && !t.IsAbstract && !t.IsInterface
                select new
                {
                    Source = i.GetGenericArguments()[0],
                    Destination = t
                }).ToList();*/

            var maps = types.SelectMany(t => t.GetInterfaces(), (t, i) => new { t, i })
                .Where(
                    type =>
                        type.i.IsGenericType && type.i.GetGenericTypeDefinition() == typeof(IMapFrom<>) && !type.t.IsAbstract && !type.t.IsInterface)
                .Select(type => new { Source = type.i.GetGenericArguments()[0], Destination = type.t });

            foreach (var map in maps)
            {
                mapperConfiguration.CreateMap(map.Source, map.Destination);
            }
        }

       /* private static void LoadReverseMappings(IEnumerable<Type> types, IProfileExpression mapperConfiguration)
        {
            var maps = (from t in types
                from i in t.GetInterfaces()
                where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>) && !t.IsAbstract && !t.IsInterface
                select new
                {
                    Destination = i.GetGenericArguments()[0],
                    Source = t
                }).ToList();

            foreach (var map in maps)
            {
                mapperConfiguration.CreateMap(map.Source, map.Destination);
            }
        }*/

        private static void LoadCustomMappings(IEnumerable<Type> types, IProfileExpression mapperConfiguration)
        {
            /*var maps = (from t in types
                from i in t.GetInterfaces()
                where typeof(IHaveCustomMappings).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface
                select (IHaveCustomMappings) Activator.CreateInstance(t)).ToList();*/

            var maps =
    types.SelectMany(t => t.GetInterfaces(), (t, i) => new { t, i })
        .Where(
            type =>
                typeof(IHaveCustomMappings).IsAssignableFrom(type.t) && !type.t.IsAbstract &&
                !type.t.IsInterface)
        .Select(type => (IHaveCustomMappings)Activator.CreateInstance(type.t));

            foreach (var map in maps)
            {
                map.CreateMappings(mapperConfiguration);
            }
        }
    }
}
