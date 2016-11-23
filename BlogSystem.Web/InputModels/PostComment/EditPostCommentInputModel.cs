using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSystem.Web.InputModels.PostComment
{
    public class EditPostCommentInputModel
    {
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [UIHint("tinymce_full")]
        public string Content { get; set; }

        public int BlogPostId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }
    }
}