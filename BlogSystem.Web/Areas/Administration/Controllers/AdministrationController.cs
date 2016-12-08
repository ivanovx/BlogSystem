namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using Common;
    using Data.UnitOfWork;
    using Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public class AdministrationController : BaseController
    {
        protected AdministrationController(IBlogSystemData data) 
            : base(data)
        {
        }
    }
}