namespace BlogSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Setting
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Value { get; set; }
    }
}