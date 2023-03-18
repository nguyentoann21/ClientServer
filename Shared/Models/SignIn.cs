using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class SignIn
    {
        [Required]
        [StringLength(50, ErrorMessage = "Your email must be less than or equal 50 characters")]
        [EmailAddress(ErrorMessage = "The email format did not supported.")]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
