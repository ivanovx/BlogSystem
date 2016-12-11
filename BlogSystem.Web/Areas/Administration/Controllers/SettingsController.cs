namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.UnitOfWork;

    public class SettingsController : AdministrationController
    {
        public SettingsController(IBlogSystemData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var settings = this.Data.Settings.All().ToList();

            return this.View(settings);
        }
    }
}