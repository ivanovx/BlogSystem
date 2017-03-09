namespace BlogSystem.Web.Infrastructure
{
    using System.Collections.Generic;

    public interface ISettingsManager
    {
        IDictionary<string, string> GetSettings();

        string this[string key] { get; }
    }
}