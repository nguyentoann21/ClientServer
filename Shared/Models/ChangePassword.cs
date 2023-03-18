using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class ChangePassword
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Wrong Email Input")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        public string CurrentPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "NewPassword is required")]
        public string NewPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "ConfirmPassword is required")]
        public string ConfirmPassword { get; set;} = string.Empty;
    }
}
