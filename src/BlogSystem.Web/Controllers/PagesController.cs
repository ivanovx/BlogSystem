namespace BlogSystem.Web.Controllers
{
    using System.Web.Mvc;

    using BlogSystem.Web.ViewModels.Pages;
    using BlogSystem.Services.Data.Pages;
    using BlogSystem.Services.Web.Mapping;

    public class PagesController : BaseController
    {
        private readonly IPagesDataService pagesData;

        public PagesController(IPagesDataService pagesData)
        {
            this.pagesData = pagesData;
        }

        public ActionResult Page(string permalink)
        {
            var page = this.pagesData.GetPage(permalink);

            if (page == null)
            {
                return this.HttpNotFound();
            }

            var model = this.Mapper.Map<PageViewModel>(page);

            return this.View(model);
        }
    }
}