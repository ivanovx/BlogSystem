namespace BlogSystem.Web.Areas.Administration.InputModels.PostComment
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class EditPostCommentInputModel
    {
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