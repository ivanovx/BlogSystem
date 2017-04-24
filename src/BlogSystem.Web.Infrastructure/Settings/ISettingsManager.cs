namespace BlogSystem.Web.Infrastructure.Settings
{
    using System;
    using System.Collections.Generic;

    public interface ISettingsManager
    {
        string this[string key] { get; }
    }
}