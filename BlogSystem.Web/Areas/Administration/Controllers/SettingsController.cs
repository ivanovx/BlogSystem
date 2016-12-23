using BlogSystem.Data.Models;

namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.UnitOfWork;

    public class SettingsController : AdministrationController
    { 
        private readonly IBlogSystemData data;

        public SettingsController(IBlogSystemData data)
        {
            this.data = data;
        }

        public ActionResult Index()
        {
            var settings = this.data.Settings.All().ToList();

            return this.View(settings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Setting setting)
        {
            this.data.Settings.Update(setting);
            this.data.SaveChanges();

            return this.RedirectToAction("Index");
        }
    }
}