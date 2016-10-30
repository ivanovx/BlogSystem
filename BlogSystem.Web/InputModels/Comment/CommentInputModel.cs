namespace BlogSystem.Web.InputModels.Comment
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class CommentInputModel
    {
        public CommentInputModel()
        {
        }

        public CommentInputModel(int blogPostId)
        {
            this.BlogPostId = blogPostId;
        }

        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [UIHint("tinymce_full")]
        public string Content { get; set; }

        public int BlogPostId { get; set; }
    }
}