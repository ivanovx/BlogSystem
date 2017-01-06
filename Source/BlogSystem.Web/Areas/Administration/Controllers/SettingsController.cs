using BlogSystem.Data.Models;

namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Base;

    public class SettingsController : AdministrationController
    {
        // GET: Administration/Settings
        public ActionResult Index()
        {
            var settings = this.Settings.All().ToList();

            return this.View(settings);
        }

        [HttpGet]
        public ActionResult Update(string id)
        {
            var setting = this.Settings.Find(id);

            return this.View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "Key,Value")] Setting setting)
        {
            if (ModelState.IsValid)
            {
                this.Settings.Update(setting);
                this.Settings.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(setting);
        }
    }
}