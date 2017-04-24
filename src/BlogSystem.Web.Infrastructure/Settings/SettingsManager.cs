namespace BlogSystem.Web.Infrastructure.Settings
{
    using System;
    using System.Linq;
    using System.Collections.Generic;   
    using Data.Models;
    using Data.Repositories;

    public class SettingsManager : ISettingsManager
    {
        private readonly IDbRepository<Setting> settingsRepository;
        private readonly Lazy<IDictionary<string, string>> settings;

        public SettingsManager(IDbRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;

            Func<IDictionary<string, string>> getSettings = delegate ()
            {
                return this.settingsRepository.All().ToDictionary(s => s.Key, s => s.Value);
            };

            this.settings = new Lazy<IDictionary<string, string>>(getSettings);
        }

        public string this[string key] => this.settings.Value[key];
    }
}