namespace BlogSystem.Web.Infrastructure
{
    using System;
    using System.Collections.Generic;

    public interface ISettingsManager
    {
        string this[string key] { get; }
    }
}