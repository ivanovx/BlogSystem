namespace BlogSystem.Web.Areas.Administration
{
    using System.Web.Mvc;

    public class AdministrationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Administration";
            }
        } 

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Administration_Default", 
                url: "Administration/{controller}/{action}/{id}", 
                defaults: new
                {
                    action = "Index",
                    controller = "Home",
                    id = UrlParameter.Optional
                },
                namespaces: new[]
                {
                    "BlogSystem.Web.Areas.Administration.Controllers"
                });
        }
    }
}