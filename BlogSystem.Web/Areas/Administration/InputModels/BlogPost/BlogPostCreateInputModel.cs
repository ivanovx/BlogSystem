namespace BlogSystem.Web.Areas.Administration.InputModels.BlogPost
{ 
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class BlogPostCreateInputModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [UIHint("tinymce_full")]
        [MinLength(10, ErrorMessage = "The {0} must be at least {1} characters long.")]
        public string Content { get; set; }
    }
}