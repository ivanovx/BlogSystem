namespace BlogSystem.Web.Areas.Administration.ViewModels.Comments
{
    using System.Collections.Generic;
    using ViewModels.Administration;

    public class IndexCommentsPageViewModel : PaginationViewModel
    {
        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}