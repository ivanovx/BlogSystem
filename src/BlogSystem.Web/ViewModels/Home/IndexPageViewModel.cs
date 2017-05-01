namespace BlogSystem.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using Common;

    public class IndexPageViewModel : PaginationViewModel
    {
        public IEnumerable<PostConciseViewModel> Posts { get; set; }
    }
}