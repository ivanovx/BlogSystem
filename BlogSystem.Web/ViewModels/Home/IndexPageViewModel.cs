namespace BlogSystem.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexPageViewModel
    {
        public IEnumerable<BlogPostConciseViewModel> Posts { get; set; }
    }
}