using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSystem.Web.Areas.Administration.ViewModels.Page
{
    public class IndexPagesViewModel
    {
        public IEnumerable<PageViewModel> Pages { get; set; }
    }
}