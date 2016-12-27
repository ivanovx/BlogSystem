namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using Web.Controllers;
    using Common;

    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public class AdministrationController : BaseController
    {

    }
}