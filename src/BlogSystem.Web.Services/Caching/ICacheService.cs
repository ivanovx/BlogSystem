namespace BlogSystem.Web.Services.Caching
{
    using System;

    public interface ICacheService
    {
        T Get <T> (string itemName, Func<T> getDataFunc, int durationInSeconds) 
            where T : class;

        void Remove(string itemName);
    }
}