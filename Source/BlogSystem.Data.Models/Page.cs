namespace BlogSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Contracts;

    public class Page : AuditInfo
    {
        private ICollection<Tag> tags;

        public Page()
        {
            this.tags = new HashSet<Tag>();
        }

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

        public virtual ICollection<Tag> SubmissionTypes
        {
            get
            {
                return this.tags;
            }
            set
            {
                this.tags = value;
            }
        }
    }
}