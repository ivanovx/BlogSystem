namespace BlogSystem.Web.Areas.Administration.Controllers.Base
{
    using System.Web.Mvc;
    using Web.Controllers;
    using Common;

    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public abstract class AdministrationController : BaseController
    {

    }
}