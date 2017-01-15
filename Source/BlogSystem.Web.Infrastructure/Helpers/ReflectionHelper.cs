namespace BlogSystem.Web.Infrastructure.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ReflectionHelper
    {
        public static ICollection<Type> GetSubClasses<T>()
        {
            return Assembly.GetCallingAssembly()
                .GetTypes()
                .Where(type => type.IsSubclassOf(typeof(T)) && !type.IsAbstract)
                .ToList();
        }
    }
}