using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Project.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [MaxLength(30, ErrorMessage = "First Name exceeds max length of 30 characters")]
        [Display(Name = "First Name")]
        public string first_name { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [MaxLength(30, ErrorMessage = "Last Name exceeds max length of 30 characters")]
        [Display(Name = "Last Name")]
        public string last_name { get; set; }

        [Required(ErrorMessage = "Date Of Birth is Required")]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime date_of_birth { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        [Display(Name = "Gender")]
        public Gender gender { get; set; }

        [Required(ErrorMessage = "Country is Required")]
        [MaxLength(50, ErrorMessage = "Country exceeds max length of 50 characters")]
        [Display(Name = "Country")]
        public string country { get; set; }

        [Required(ErrorMessage = "Phone Number is Required")]
        [Display(Name = "Phone Number")]
        [Phone]
        public string phone_number { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [MaxLength(150, ErrorMessage = "Email exceeds max length of 150 characters")]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "Role is Required")]
        [Display(Name = "Role")]
        public Role role { get; set; }

        [Display(Name = "Image Name")]
        public string? ImageFileName { get; set; }

        [Display(Name = "Image")]
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }

    public enum Role
    {
        Admin,
        User
    }
}
