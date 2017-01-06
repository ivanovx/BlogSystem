namespace BlogSystem.Services.Cache
{
    using System;

    public interface ICacheService : IService
    {
        T Get<T>(string itemName, Func<T> getDataFunc, int durationInSeconds);

        void Remove(string itemName);
    }
}