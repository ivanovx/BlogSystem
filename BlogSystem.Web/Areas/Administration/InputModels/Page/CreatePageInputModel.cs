namespace BlogSystem.Web.Areas.Administration.InputModels.Page
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class CreatePageInputModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [UIHint("tinymce_full")]
        [AllowHtml]
        [DataType(DataType.Html)]
        public string Content { get; set; }

        [Required]
        public bool VisibleInMenu { get; set; }
    }
}