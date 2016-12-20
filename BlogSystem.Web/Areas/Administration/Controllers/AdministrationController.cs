namespace BlogSystem.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using Common;
    using Data.UnitOfWork;
    using Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public class AdministrationController : BaseController
    {
        /*public AdministrationController(IBlogSystemData data)
        {
            this.Data = data;
        }

        public IBlogSystemData Data { get; }*/
    }
}