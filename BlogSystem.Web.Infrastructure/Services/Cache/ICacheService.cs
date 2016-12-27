using System;

namespace BlogSystem.Web.Infrastructure.Services.Cache
{
    public interface ICacheService : IService
    {
        T Get<T>(string itemName, Func<T> getDataFunc, int durationInSeconds);

        void Remove(string itemName);
    }
}
