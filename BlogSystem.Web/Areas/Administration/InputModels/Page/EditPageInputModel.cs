namespace BlogSystem.Web.Areas.Administration.InputModels.Page
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class EditPageInputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [UIHint("tinymce_full")]
        [AllowHtml]
        [DataType(DataType.Html)]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}