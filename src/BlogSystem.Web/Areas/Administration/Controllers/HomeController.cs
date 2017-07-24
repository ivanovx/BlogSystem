namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Reflection;

    public class HomeController : AdministrationController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 6 * 10 * 60)]
        public PartialViewResult AdminMenu()
        {
            var menuItems = Assembly
                .GetAssembly(typeof(AdministrationController))
                .GetTypes()
                .Where(type => type.IsSubclassOf(typeof(AdministrationController)) && !type.IsAbstract)
                .Select(c => c.Name.Replace("Controller", string.Empty))
                .ToList();

            return this.PartialView(menuItems);
        }
    }
}