namespace BlogSystem.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexPageViewModel
    {
        public IEnumerable<PostConciseViewModel> Posts { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}