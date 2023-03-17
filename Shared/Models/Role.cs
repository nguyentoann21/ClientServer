namespace Shared.Models
{
    public class Role
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
