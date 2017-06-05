namespace BlogSystem.Services.Web.Caching
{
    using System;
    using System.Web;
    using System.Web.Caching;

    public class CacheService : ICacheService
    {
        private readonly object lockObject = new object();

        public T Get<T> (string name, Func<T> getDataFunc, int durationInSeconds) 
            where T : class
        {
            lock (lockObject)
            {
                if (HttpRuntime.Cache[name] == null)
                {
                    var time = DateTime.Now.AddSeconds(durationInSeconds);
                    var item = getDataFunc();

                    HttpRuntime
                        .Cache
                        .Insert(name, item, null, time, Cache.NoSlidingExpiration);
                }
            }

            return HttpRuntime.Cache[name] as T;
        }

        public void Remove(string name)
        {
            HttpRuntime.Cache.Remove(name);
        }
    }
}