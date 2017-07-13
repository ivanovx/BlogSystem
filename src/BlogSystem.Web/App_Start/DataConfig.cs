namespace BlogSystem.Web
{
    using System.Data.Entity;

    using BlogSystem.Data;
    using BlogSystem.Data.Migrations;

    public class DataConfig
    {
        internal static void ConfigureDatabase()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }
    }
}