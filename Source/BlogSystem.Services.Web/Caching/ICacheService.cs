namespace BlogSystem.Services.Web.Caching
{
    using System;

    public interface ICacheService
    {
        T Get<T>(string itemName, Func<T> getDataFunc, int durationInSeconds);
    }
}