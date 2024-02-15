using System.ComponentModel.DataAnnotations;

namespace CRUD_Project.Models
{
    public class BindingModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(30, ErrorMessage = "First Name exceeds max length of 30 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(150, ErrorMessage = "Email exceeds max length of 150 characters")]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
