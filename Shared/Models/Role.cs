using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class Role
    {
        [Key]
        public string RoleId { get; set; } = string.Empty;
        [Required]
        public string RoleName { get; set; } = string.Empty;
        public ICollection<Account> Accounts { get; set; } = new HashSet<Account>();
    }
}
