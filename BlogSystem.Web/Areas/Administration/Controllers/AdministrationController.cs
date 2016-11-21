namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using BlogSystem.Common;
    using BlogSystem.Data.UnitOfWork;
    using BlogSystem.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public class AdministrationController : BaseController
    {
        protected AdministrationController(IBlogSystemData data) 
            : base(data)
        {
        }
    }
}