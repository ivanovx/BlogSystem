namespace BlogSystem.Web.ViewModels.Home
{
    using BlogSystem.Web.ViewModels.Common;

    using System.Collections.Generic;

    public class IndexPageViewModel : PaginationViewModel
    {
        public IEnumerable<PostConciseViewModel> Posts { get; set; }
    }
}