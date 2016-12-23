namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
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

        [HttpGet]
        public ActionResult Edit(string key)
        {
            /*if (key == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/

            var setting = this.data.Settings.All().FirstOrDefault(s => s.Key == key);

            /*if (setting == null)
            {
                return this.HttpNotFound();
            }*/


            return this.View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Setting setting)
        {
            if (ModelState.IsValid)
            {
                this.data.Settings.Update(setting);
                this.data.Settings.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(setting);
        }
    }
}