namespace BlogSystem.Web.Areas.Administration.InputModels.Page
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class CreatePageInputModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [UIHint("tinymce_full")]
        [AllowHtml]
        [DataType(DataType.Html)]
        public string Content { get; set; }
    }
}