using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BlogSystem.Web.Areas.Administration.InputModels.PostComment
{
    public class EditPostCommentInputModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [UIHint("tinymce_full")]
        public string Content { get; set; }

        [Required]
        public int BlogPostId { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}