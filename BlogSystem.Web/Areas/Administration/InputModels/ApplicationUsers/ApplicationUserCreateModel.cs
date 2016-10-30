namespace BlogSystem.Web.Areas.Administration.InputModels.ApplicationUsers
{
    using System.ComponentModel.DataAnnotations;

    public class ApplicationUserCreateModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}