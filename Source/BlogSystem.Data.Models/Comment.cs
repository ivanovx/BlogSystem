namespace BlogSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Contracts;

    public class Comment : AuditInfo, IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Html)]
        [MinLength(10, ErrorMessage = "The {0} must be at least {1} characters long.")]
        public string Content { get; set; }

        [Required]
        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}