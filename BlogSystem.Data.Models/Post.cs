namespace BlogSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Contracts;

    public class Post : AuditInfo, IDeletableEntity
    {
        private ICollection<PostComment> comments;

        public Post()
        {
            this.comments = new HashSet<PostComment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Html)]
        [MinLength(10, ErrorMessage = "The {0} must be at least {1} characters long.")]
        public string Content { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual ApplicationUser Author { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<PostComment> Comments
        {
            get
            {
                return this.comments;
            }

            set
            {
                this.comments = value;
                
            }
        }
    }
}