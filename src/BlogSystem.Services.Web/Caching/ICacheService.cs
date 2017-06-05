namespace BlogSystem.Services.Web.Caching
{
    using System;

    public interface ICacheService : IWebService
    {
        T Get <T> (string itemName, Func<T> getDataFunc, int durationInSeconds) 
            where T : class;

        void Remove(string name);
    }
}