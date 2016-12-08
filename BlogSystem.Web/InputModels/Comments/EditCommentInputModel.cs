namespace BlogSystem.Web.InputModels.Comments
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class EditCommentInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [UIHint("tinymce_full")]
        public string Content { get; set; }

        [Required]
        public int BlogPostId { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}