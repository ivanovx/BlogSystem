namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using Common;
    using Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public class AdministrationController : BaseController
    {

    }
}