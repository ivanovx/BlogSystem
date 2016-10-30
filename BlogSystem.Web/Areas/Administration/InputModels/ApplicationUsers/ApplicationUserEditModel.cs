namespace BlogSystem.Web.Areas.Administration.InputModels.ApplicationUsers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ApplicationUserEditModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string PasswordHash { get; set; }

        [Required]
        public string SecurityStamp { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}