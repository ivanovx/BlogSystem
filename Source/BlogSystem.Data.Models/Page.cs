namespace BlogSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Contracts;

    public class Page : AuditInfo
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        [DataType(DataType.Html)]
        public string Content { get; set; }

        public string Permalink { get; set; }

        public bool VisibleInMenu { get; set; }

        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual ApplicationUser Author { get; set; }
    }
}