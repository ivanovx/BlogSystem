namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.UnitOfWork;

    public class SettingsController : AdministrationController
    {
        public SettingsController(IBlogSystemData data)
        {
            this.Data = data;
        }

        public IBlogSystemData Data { get; }

        public ActionResult Index()
        {
            var settings = this.Data.Settings.All().ToList();

            return this.View(settings);
        }
    }
}