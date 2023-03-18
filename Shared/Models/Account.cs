using Imagekit.Constant;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class Account
    {
        [Key]
        [Range(1, int.MaxValue)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Your email must be less than or equal 50 characters")]
        [EmailAddress(ErrorMessage = "The email format did not supported.")]
        public string Email { get; set; } = string.Empty;

        [Required]
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

        [ForeignKey(nameof(RoleManagers))]
        public string RoleID { get; set; } = string.Empty;
        public Role? RoleManagers { get; set; }
    }
}
