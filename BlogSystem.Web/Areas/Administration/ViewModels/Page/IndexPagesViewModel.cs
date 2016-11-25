namespace BlogSystem.Web.Areas.Administration.ViewModels.Page
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class IndexPagesViewModel
    {
        public IEnumerable<PageViewModel> Pages { get; set; }
    }
}