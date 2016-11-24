using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSystem.Data.Models;

namespace BlogSystem.Web.Areas.Administration.InputModels.Page
{
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