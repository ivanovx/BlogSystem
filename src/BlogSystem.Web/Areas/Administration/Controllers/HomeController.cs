namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;

    public class HomeController : AdministrationController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [ChildActionOnly]
        public PartialViewResult AdminMenu()
        {
            var items = Assembly
                .GetAssembly(typeof(AdministrationController))
                .GetTypes()
                .Where(type => type.IsSubclassOf(typeof(AdministrationController)) && !type.IsAbstract)
                .Select(c => c.Name.Replace("Controller", string.Empty))
                .ToList();

            return this.PartialView(items);
        }
    }
}