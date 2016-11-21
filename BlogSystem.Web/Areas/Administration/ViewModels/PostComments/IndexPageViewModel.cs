using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSystem.Web.Areas.Administration.ViewModels.PostComments
{
    public class IndexPageViewModel
    {
        public IEnumerable<PostCommentViewModel> Comments { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public int NextPage
        {
            get
            {
                if (this.CurrentPage >= this.PagesCount)
                {
                    return 1;
                }

                return this.CurrentPage + 1;
            }
        }

        public int PreviousPage
        {
            get
            {
                if (this.CurrentPage <= 1)
                {
                    return this.PagesCount;
                }

                return this.CurrentPage - 1;
            }
        }
    }
}