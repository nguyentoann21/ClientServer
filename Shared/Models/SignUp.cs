using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class SignUp
    {
        [Required(ErrorMessage ="Email can not be blank")]
        [StringLength(50, ErrorMessage = "Your email must be less than or equal 50 characters")]
        [EmailAddress(ErrorMessage = "The email format did not supported.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password can not be blank")]
        [StringLength(6, ErrorMessage = "Your Password must be less than 24 or equal 6 characters")]
        public string Password { get; set; } = string.Empty;
        [Required]
        [StringLength(50, ErrorMessage = "Your full name must be less than or equal 50 characters")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(10, ErrorMessage = "Your Phone number must be include 10 characters")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "Your Address must be less than or equal 100 characters")]
        public string Address { get; set; } = string.Empty;
    }
}
