namespace BlogSystem.Services.Cache
{
    using System;
    using System.Web;
    using System.Web.Caching;

    public class CacheService : ICacheService
    {
        private readonly object lockObject = new object();

        public T Get<T>(string name, Func<T> getDataFunc, int durationInSeconds)
        {
            if (HttpRuntime.Cache[name] == null)
            {
                lock (lockObject)
                {
                    if (HttpRuntime.Cache[name] == null)
                    {
                        var data = getDataFunc();
                        var time = DateTime.Now.AddSeconds(durationInSeconds);

                        HttpRuntime.Cache.Insert(name, data, null, time, Cache.NoSlidingExpiration);
                    }
                }
            }

            return (T) HttpRuntime.Cache[name];
        }

        public void Remove(string name)
        {
            HttpRuntime.Cache.Remove(name);
        }
    }
}