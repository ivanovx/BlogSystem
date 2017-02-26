namespace BlogSystem.Web.Infrastructure.Caching
{
    using System;
    using System.Web;
    using System.Web.Caching;

    public class CacheService : ICacheService
    {
        private readonly object lockObject = new object();

        public T Get<T> (string name, Func<T> getDataFunc, int durationInSeconds)
        {
            lock (lockObject)
            {
                if (HttpRuntime.Cache[name] == null)
                {
                    var time = DateTime.Now.AddSeconds(durationInSeconds);

                    HttpRuntime.Cache.Insert(name, getDataFunc(), null, time, Cache.NoSlidingExpiration);
                }
            }

            return (T) HttpRuntime.Cache[name];
        }
    }
}