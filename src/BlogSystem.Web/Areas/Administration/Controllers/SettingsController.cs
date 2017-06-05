namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using BlogSystem.Data.Models;
    using BlogSystem.Data.Repositories;

    public class SettingsController : AdministrationController
    {
        private readonly IDbRepository<Setting> settingsData;

        public SettingsController(IDbRepository<Setting> settingsData)
        {
            this.settingsData = settingsData;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var settings = this.settingsData.All().ToList();

            return this.View(settings);
        }

        [HttpGet]
        public ActionResult Update(string id)
        {
            if (id == null)
            {
                return this.HttpNotFound();
            }

            var setting = this.settingsData.Find(id);

            return this.View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "Key,Value")] Setting setting)
        {
            if (setting != null && ModelState.IsValid)
            {
                this.settingsData.Update(setting);
                this.settingsData.SaveChanges();

                this.cache.Remove("Settings");

                return this.RedirectToAction("Index");
            }

            return this.View(setting);
        }
    }
}