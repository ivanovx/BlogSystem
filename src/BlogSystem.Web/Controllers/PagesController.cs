namespace BlogSystem.Web.Controllers
{
    using System.Web.Mvc;
    using ViewModels.Pages;
    using BlogSystem.Services.Web.Mapping;
    using BlogSystem.Services.Data.Pages;

    public class PagesController : BaseController
    {
        private readonly IPagesDataService pagesData;
        private readonly IMappingService mappingService;

        public PagesController(IPagesDataService pagesData, IMappingService mappingService)
        {
            this.pagesData = pagesData;
            this.mappingService = mappingService;
        }

        [HttpGet]
        public ActionResult Page(string permalink)
        {
            var page = this.pagesData.GetPage(permalink);

            if (page == null)
            {
                return this.HttpNotFound();
            }

            var model = this.mappingService.Map<PageViewModel>(page);

            return View(model);
        }
    }
}