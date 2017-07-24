namespace BlogSystem.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using BlogSystem.Web.ViewModels;

    public class IndexPageViewModel : PaginationViewModel
    {
        public IEnumerable<PostConciseViewModel> Posts { get; set; }
    }
}