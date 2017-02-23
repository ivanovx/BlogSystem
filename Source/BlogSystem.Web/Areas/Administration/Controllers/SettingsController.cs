namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Base;
    using Data.Models;
    using Data.Repositories;

    public class SettingsController : AdministrationController
    {
        private readonly IDbRepository<Setting> settingsRepository;

        public SettingsController(IDbRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public ActionResult Index()
        {
            var settings = this.settingsRepository.All().ToList();

            return this.View(settings);
        }

        [HttpGet]
        public ActionResult Update(string id)
        {
            var setting = this.settingsRepository.Find(id);

            return this.View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Setting setting)
        {
            if (ModelState.IsValid)
            {
                this.settingsRepository.Update(setting);
                this.settingsRepository.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(setting);
        }
    }
}