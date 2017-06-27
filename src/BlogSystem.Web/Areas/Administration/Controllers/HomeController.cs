namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Reflection;

    using BlogSystem.Web.Areas.Administration.ViewModels;
    using System;

    public class HomeController : AdministrationController
    {
        public ActionResult Index()
        {
            var baseInfo = new BaseInfoViewModel
            {
                Browser = this.Request.Browser.Browser,
                IpAddress = this.Request.UserHostAddress,
                Platform  = Environment.OSVersion.VersionString
            };

            return this.View(baseInfo);
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