namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Common;
    using System;
    using Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public abstract class AdministrationController : BaseController
    {
        
    }
}