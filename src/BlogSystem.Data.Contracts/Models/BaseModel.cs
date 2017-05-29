namespace BlogSystem.Data.Contracts.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class BaseModel<TKey> : IAuditInfo, IDeletableEntity
    {
        [Key]
        public TKey Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }

        [NotMapped]
        public bool PreserveCreatedOn { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DeletedOn { get; set; }
    }
}