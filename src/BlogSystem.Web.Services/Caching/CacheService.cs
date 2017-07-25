namespace BlogSystem.Web.Services.Caching
{
    using System;
    using System.Web;
    using System.Web.Caching;

    public class CacheService : ICacheService
    {
        private readonly object lockObject = new object();

        public T Get<T> (string itemName, Func<T> getDataFunc, int durationInSeconds) 
            where T : class
        {
            lock (lockObject)
            {
                if (HttpRuntime.Cache[itemName] == null)
                {
                    var time = DateTime.Now.AddSeconds(durationInSeconds);
                    var item = getDataFunc();

                    HttpRuntime
                        .Cache
                        .Insert(itemName, item, null, time, Cache.NoSlidingExpiration);
                }
            }

            return HttpRuntime.Cache.Get(itemName) as T;
        }

        public void Remove(string itemName)
        {
            HttpRuntime.Cache.Remove(itemName);
        }
    }
}