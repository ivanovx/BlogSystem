namespace BlogSystem.Web.Controllers
{
    using System.Web.Mvc;

    using BlogSystem.Web.Infrastructure.Attributes;

    [HandleError]
    [PassSettingsToViewData]
    public abstract class BaseController : Controller
    {
      
    }
}