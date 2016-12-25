namespace BlogSystem.Web.InputModels.Comments
{
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CreateCommentInputModel : IMapTo<Comment>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        [UIHint("tinymce_full")]
        public string Content { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}