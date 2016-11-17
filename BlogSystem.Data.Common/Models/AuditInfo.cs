namespace BlogSystem.Data.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class AuditInfo : IAuditInfo
    {
        public DateTime CreatedOn { get; set; }

        [NotMapped]
        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}