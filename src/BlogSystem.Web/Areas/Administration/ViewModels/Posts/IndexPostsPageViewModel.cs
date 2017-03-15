﻿namespace BlogSystem.Web.Areas.Administration.ViewModels.Posts
{
    using System.Collections.Generic;
    using ViewModels.Administration;

    public class IndexPostsPageViewModel : PaginationViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; }
    }
}